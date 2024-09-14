using ERP.Share.Abstractions.Shared;

namespace ERP.Application.UseCases.Users.CreateUser;

public sealed record CreateUserCommand(
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        string Password) : ICommand;

