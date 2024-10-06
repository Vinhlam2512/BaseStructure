namespace LETOS.Domain.Exceptions;

public abstract class BadRequestException : DomainException
{
    protected BadRequestException(string message)
        : base("Bad Request", message)
    {
    }

    protected BadRequestException(string title, string message)
        : base(title, message)
    {
    }
}

