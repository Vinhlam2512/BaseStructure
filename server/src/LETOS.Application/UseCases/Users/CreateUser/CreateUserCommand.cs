using LETOS.Share.Abstractions.Shared;

namespace LETOS.Application.UseCases.Users.CreateUser;

public sealed record CreateUserCommand(
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        string Password) : ICommand;

