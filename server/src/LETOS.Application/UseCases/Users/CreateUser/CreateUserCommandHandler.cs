using LETOS.Application.Abstractions.Authentication;
using LETOS.Domain.Entities.Users;
using LETOS.Share.Abstractions.Shared;

namespace LETOS.Application.UseCases.Users.CreateUser;
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Name name = Name.Create(request.FirstName, request.LastName, request.UserName);

            string passwordHashed = _passwordService.HashPassword(request.Password);

            var user = User.Create(
                name,
                new Email(request.Email),
                passwordHashed);


            await _userRepository.Add(user);
            return Result.Success();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
