using FluentValidation;
using LETOS.Application.UseCases.Users.GetUser;

namespace LETOS.Application.UseCases.Users.CreateUser;
public class GetUserValidator : AbstractValidator<GetUserQuery>
{
    public GetUserValidator()
    {

    }
}
