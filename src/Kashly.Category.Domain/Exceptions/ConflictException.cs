using System.Net;

namespace Kashly.Category.Domain.Exceptions;

public class ConflictException(string message) : KashlyException(message)
{
    public override int StatusCode => (int)HttpStatusCode.Conflict;
    public override List<string> GetErrors() => [Message];
}
