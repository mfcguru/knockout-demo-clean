using System.Net;

namespace KnockoutDemo.Source.Domain.BusinessRules
{
    public class InvalidRowException : BusinessRulesException
    {
        private const string message = "Parse error: Invalid row in line {0}. Please check number of fields must be correct.";

        public InvalidRowException(int lineNo) : base(HttpStatusCode.BadRequest, string.Format(message, lineNo)) { }
    }
}
