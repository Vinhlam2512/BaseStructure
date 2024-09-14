using FluentValidation;

namespace ERP.Application.UseCases.Indentity.Login;
public class LogInUserValidator : AbstractValidator<LogInUserCommand>
{
    public LogInUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
