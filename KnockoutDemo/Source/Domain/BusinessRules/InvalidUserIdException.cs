using System.Net;

namespace KnockoutDemo.Source.Domain.BusinessRules
{
    public class InvalidUserIdException : BusinessRulesException
    {
        private const string message = "Parse error: Invalid UserId field value in line no. {0}. Possibly non-numeric or there is already an exisiting UserId in the database.";

        public InvalidUserIdException(int lineNo) : base(HttpStatusCode.BadRequest, string.Format(message, lineNo)) { }
    }
}
