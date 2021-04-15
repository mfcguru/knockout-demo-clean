using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnockoutDemo.Source.Domain.Controllers
{
    using KnockoutDemo.Source.Domain.BusinessRules;
    using KnockoutDemo.Source.Domain.UseCases.DeleteAllUsers;
    using KnockoutDemo.Source.Domain.UseCases.GetAllUsers;
    using KnockoutDemo.Source.Domain.UseCases.UploadCsvFile;

    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IMediator mediator;

        public ApiController(IMediator mediator) => this.mediator = mediator;

        [DisableRequestSizeLimit]
        [HttpPost("csv/upload")]
        public async Task<ActionResult> UploadCsvFile([FromForm] IFormFile file)
        {
            try
            {
                await mediator.Send(new UploadCsvFileCommand(file));
                return Ok();
            }
            catch (BusinessRulesException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await mediator.Send(new GetAllUsersCommand());

            return Ok(result);
        }

        [HttpDelete("report")]
        public async Task<ActionResult> DeleteAllUsers()
        {
            await mediator.Send(new DeleteAllUsersCommand());

            return Ok();
        }
    }
}
