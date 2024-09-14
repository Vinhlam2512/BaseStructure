using ERP.Share.Abstractions.Shared;

namespace ERP.Application.UseCases.Indentity.Login;
public sealed record LogInUserCommand(string UserName, string Password)
    : ICommand<LogInUserResponse>;

