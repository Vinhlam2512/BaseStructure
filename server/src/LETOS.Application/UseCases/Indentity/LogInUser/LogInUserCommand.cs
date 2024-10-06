using LETOS.Share.Abstractions.Shared;

namespace LETOS.Application.UseCases.Indentity.Login;
public sealed record LogInUserCommand(string UserName, string Password)
    : ICommand<LogInUserResponse>;

