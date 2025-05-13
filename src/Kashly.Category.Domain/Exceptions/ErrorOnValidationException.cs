using System.Net;

namespace Kashly.Category.Domain.Exceptions;

public class ErrorOnValidationException(List<string> errorMessages) : KashlyException(string.Empty)
{
    public readonly List<string> Errors = errorMessages;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public override List<string> GetErrors() => Errors;
}
