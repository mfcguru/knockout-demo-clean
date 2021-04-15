using System.Net;

namespace KnockoutDemo.Source.Domain.BusinessRules
{
    public class InvalidAgeException : BusinessRulesException
    {
        private const string message = "Parse error: Invalid Age field value in line no. {0}. Possigbly Age is non-numeric or the value is less than zero.";

        public InvalidAgeException(int lineNo) : base(HttpStatusCode.BadRequest, string.Format(message, lineNo)) { }
    }
}
