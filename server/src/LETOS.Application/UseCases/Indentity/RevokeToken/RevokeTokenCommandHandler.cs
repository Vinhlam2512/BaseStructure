using LETOS.Application.Abstractions.Authentication;
using LETOS.Application.Abstractions.Caching;
using LETOS.Share.Abstractions.Shared;
using LETOS.Share.Responses.Token;

namespace LETOS.Application.UseCases.Indentity.RevokeToken;
public class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand>
{
    private readonly ICacheService _cacheService;
    private readonly IJwtService _jwtService;
    private readonly IUserContext _userContext;


    public RevokeTokenCommandHandler(ICacheService cacheService, IJwtService jwtService, IUserContext userContext)
    {
        _cacheService = cacheService;
        _jwtService = jwtService;
        _userContext = userContext;
    }

    public async Task<Result> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {

        string id = _userContext.UserId.ToString();
        var tokenResponse = await _cacheService.GetAsync<TokenResponse>(id, cancellationToken) ?? throw new Exception("Can not get value from Redis");

        await _cacheService.RemoveAsync(id, cancellationToken);

        return Result.Success();
    }
}
