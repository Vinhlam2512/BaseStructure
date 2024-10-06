using FluentValidation;
using LETOS.Share.Extensions;

namespace LETOS.Application.UseCases.Users.CreateUser;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(5);

    }
}
