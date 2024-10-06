using LETOS.Application.Abstractions.Authentication;
using LETOS.Application.Abstractions.Caching;
using LETOS.Share.Abstractions.Shared;
using LETOS.Share.Responses.Token;
using static LETOS.Domain.Exceptions.IdentityException;

namespace LETOS.Application.UseCases.Indentity.RefreshToken;
public class RefreshTokenQueryHandler : IQueryHandler<RefreshTokenQuery, TokenResponse>
{
    private readonly IJwtService _jwtService;
    private readonly ICacheService _cacheService;
    private readonly IUserContext _userContext;

    public RefreshTokenQueryHandler(IJwtService jwtService, ICacheService cacheService, IUserContext userContext)
    {
        _jwtService = jwtService;
        _cacheService = cacheService;
        _userContext = userContext;
    }

    public async Task<Result<TokenResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var accessToken = request.AccessToken;
        var refreshToken = request.RefreshToken;

        var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
        string id = _userContext.UserId.ToString();

        var TokenResponse = await _cacheService.GetAsync<TokenResponse>(id, cancellationToken);
        if (TokenResponse is null || TokenResponse.RefreshToken != refreshToken || TokenResponse.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new TokenException("Request token invalid!");
        }

        var newAccessToken = _jwtService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        var newTokenResponse = new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddMinutes(90)
        };

        await _cacheService.SetAsync(id, newTokenResponse, TimeSpan.FromMinutes(90), cancellationToken: cancellationToken);

        return Result.Success(newTokenResponse);
    }
}
