using Asp.Versioning;
using LETOS.Api.Abstractions;
using LETOS.Application.UseCases.Indentity.RefreshToken;
using LETOS.Application.UseCases.Indentity.RevokeToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LETOS.Api.Controllers.V1;
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/token")]
public class TokenController : ApiController
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TokenController(ISender sender, IHttpContextAccessor httpContextAccessor)
        : base(sender)
    {
        _httpContextAccessor = httpContextAccessor;

    }
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenQuery token)
    {
        var tokens = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();


        string accessToken = tokens.Split(" ")[1];
        //var accessToken = HttpContext.Request.Headers["Authorization"];
        //accessToken = accessToken.ToString().Split(" ")[1];
        RefreshTokenQuery refreshToken = new RefreshTokenQuery(accessToken, token.RefreshToken);
        var result = await Sender.Send(refreshToken);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("revoke")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RevokeToken()
    {
        RevokeTokenCommand revokeToken = new RevokeTokenCommand();
        var result = await Sender.Send(revokeToken);

        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Ok(result);
    }
}
