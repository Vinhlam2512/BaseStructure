
using LETOS.Application.Abstractions.Authentication;
using LETOS.Application.Abstractions.Caching;
using LETOS.Application.UseCases.Users.LogOutUser;
using LETOS.Share.Abstractions.Shared;
using LETOS.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
namespace LETOS.Application.UseCases.Users.GetUser;
public sealed class LogOutCommandHandler : ICommandHandler<LogOutUserCommand>
{
    private readonly IUserContext _userContext;
    private readonly ICacheService _cacheService;

    private readonly UserManager<User> _userManager;

    public LogOutCommandHandler(IUserContext userContext, ICacheService cacheService)
    {
        _userContext = userContext;
        _cacheService = cacheService;
    }

    public async Task<Result> Handle(LogOutUserCommand request, CancellationToken cancellationToken)
    {
        string id = _userContext.UserId.ToString();
        await _cacheService.RemoveAsync(id, cancellationToken);
        return Result.Success();
    }
}
