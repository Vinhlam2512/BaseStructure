using System.Security.Claims;
using LETOS.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace LETOS.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtService _jwtService;

    public UserContext(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtService = jwtService;
    }

    public Guid UserId => GetUserId();

    public string UserName => GetUsername();


    private Guid GetUserId()
    {
        var claims = GetClaim();
        Guid userId = ClaimsPrincipalExtensions.GetUserId(claims);
        return userId;
    }

    private string GetUsername()
    {
        var claims = GetClaim();
        string username = ClaimsPrincipalExtensions.GetUserName(claims);
        return username;
    }

    private ClaimsPrincipal GetClaim()
    {
        var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new ApplicationException("Access token not found in headers");
        }

        string token = accessToken.Split(" ")[1];

        ClaimsPrincipal claims = _jwtService.GetPrincipalFromExpiredToken(token)
            ?? throw new ApplicationException("Invalid access token");
        return claims;
    }
}

