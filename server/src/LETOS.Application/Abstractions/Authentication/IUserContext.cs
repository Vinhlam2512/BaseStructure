namespace LETOS.Application.Abstractions.Authentication;
public interface IUserContext
{
    Guid UserId { get; }

    string UserName { get; }
}

