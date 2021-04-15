using System.Net;

namespace KnockoutDemo.Source.Domain.BusinessRules
{
    public class RequiredFieldsException : BusinessRulesException
    {
        private const string message = "Parse error: Required fields are missing.";

        public RequiredFieldsException(int lineNo) : base(HttpStatusCode.BadRequest, string.Format(message, lineNo)) { }
    }
}
