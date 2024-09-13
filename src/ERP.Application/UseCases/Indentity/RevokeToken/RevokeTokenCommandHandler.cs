using ERP.Application.Abstractions.Authentication;
using ERP.Application.Abstractions.Caching;
using ERP.Share.Abstractions.Shared;
using ERP.Share.Responses.Token;

namespace ERP.Application.UseCases.Indentity.RevokeToken;
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
