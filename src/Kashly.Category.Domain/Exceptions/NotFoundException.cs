using System.Net;

namespace Kashly.Category.Domain.Exceptions;

public class NotFoundException(string message) : KashlyException(message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public override List<string> GetErrors() => [Message];
}
