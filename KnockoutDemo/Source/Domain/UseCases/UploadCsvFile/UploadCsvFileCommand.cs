using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KnockoutDemo.Source.Domain.UseCases.UploadCsvFile
{
    using KnockoutDemo.Entities;
    using KnockoutDemo.Source.Domain.BusinessRules;

    public class UploadCsvFileCommand : IRequest
    {
        private readonly IFormFile file;

        public UploadCsvFileCommand(IFormFile file) => this.file = file;

        public class UploadCsvFileCommandHandler : IRequestHandler<UploadCsvFileCommand>
        {
            private readonly DataContext context;

            public UploadCsvFileCommandHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(UploadCsvFileCommand request, CancellationToken cancellationToken)
            {
                var items = ReadAsList(request.file);
                var lineNo = 1;
                foreach (var item in items)
                {
                    var data = item.Split(',');
                    await Validate(data, lineNo);
                    context.Users.Add(new User
                    {
                        UserId = int.Parse(data[0]),
                        FirstName = data[1],
                        LastName = data[2],
                        Age = int.Parse(data[3])
                    });
                    lineNo++;
                }
                await context.SaveChangesAsync();
                return Unit.Value;
            }

            private async Task Validate(string[] data, int lineNo)
            {
                if (data.Length != 4)
                {
                    throw new InvalidRowException(lineNo);
                }

                if (data[0].Length == 0 ||
                    data[1].Length == 0 ||
                    data[2].Length == 0 ||
                    data[3].Length == 0)
                {
                    throw new RequiredFieldsException(lineNo);
                }

                int userId;
                if (int.TryParse(data[0], out userId) == false)
                {
                    throw new InvalidUserIdException(lineNo);
                }
                if (await context.Users.AnyAsync(o => o.UserId == userId) == true)
                {
                    throw new InvalidUserIdException(lineNo);
                }

                int age;
                if (int.TryParse(data[3], out age) == false)
                {
                    throw new InvalidAgeException(lineNo);
                }
                if (age < 0)
                {
                    throw new InvalidAgeException(lineNo);
                }
            }

            private List<string> ReadAsList(IFormFile file)
            {
                var result = new List<string>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var header = reader.ReadLine().Replace(" ", "").Split(',');
                    if (header.Length != 4)
                    {
                        throw new InvalidCsvHeaderException();
                    }
                    if (header[0].ToLower() != "userid" ||
                        header[1].ToLower() != "firstname" ||
                        header[2].ToLower() != "lastname" ||
                        header[3].ToLower() != "age")
                    {
                        throw new InvalidCsvHeaderException();
                    }
                    while (reader.Peek() >= 0)
                    {
                        result.Add(reader.ReadLine());
                    }
                }
                return result;
            }
        }
    }
}
