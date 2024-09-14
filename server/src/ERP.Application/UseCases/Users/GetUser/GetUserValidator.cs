using FluentValidation;
using ERP.Application.UseCases.Users.GetUser;

namespace ERP.Application.UseCases.Users.CreateUser;
public class GetUserValidator : AbstractValidator<GetUserQuery>
{
    public GetUserValidator()
    {

    }
}
