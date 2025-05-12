namespace Kashly.Category.Domain.Exceptions;

public abstract class KashlyException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
