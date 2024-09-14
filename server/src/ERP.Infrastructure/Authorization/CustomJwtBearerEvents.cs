using ERP.Application.Abstractions.Authentication;
using ERP.Application.Abstractions.Caching;
using ERP.Share.Responses.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace ERP.Infrastructure.Authorization;
public class CustomJwtBearerEvents : JwtBearerEvents
{
    private readonly ICacheService _cacheService;
    private readonly IUserContext _userContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomJwtBearerEvents(ICacheService cacheService, IUserContext userContext, IHttpContextAccessor httpContextAccessor)
    {
        _cacheService = cacheService;
        _userContext = userContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task TokenValidated(TokenValidatedContext context)
    {
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(token))
        {
            context.Fail("Authentication fail. Dont have token!");
        }

        string accessToken = token.Split(" ")[1];
        if (!string.IsNullOrEmpty(accessToken))
        {
            var id = _userContext.UserId.ToString();
            var authenticated = await _cacheService.GetAsync<TokenResponse>(id);

            if (authenticated is null || authenticated.AccessToken != accessToken)
            {
                context.Response.Headers.Append("IS-TOKEN-REVOKED", "true");
                context.Fail("Authentication fail. Token has been revoked!");
            }
        }
        else
        {
            context.Fail("Authentication fail.");
        }
    }
}
