using Carter;
using ERP.Application.UseCases.Indentity;
using ERP.Application.UseCases.Indentity.RefreshToken;
using ERP.Application.UseCases.Indentity.RevokeToken;
using ERP.Contract.Abstractions.Shared;
using ERP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.APIs;

public class TokenApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "api/v{version:apiVersion}/token";
    private const string Name = "Token";
    private const int ApiVersion = ApiVersions.V1;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var workFromHomeApplicationV1 = app.NewVersionedApi(Name)
            .MapGroup(BaseUrl).HasApiVersion(ApiVersion).RequireAuthorization();

        workFromHomeApplicationV1.MapPost("refresh", RefreshToken);
        workFromHomeApplicationV1.MapPost("revoke", RevokeToken);
    }

    private static async Task<IResult> RevokeToken(ISender sender, IHttpContextAccessor _httpContextAccessor, [FromBody] RefreshTokenQuery token)
    {
        var tokens = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        string accessToken = tokens?.Split(" ")[1];
        var refreshToken = new RefreshTokenQuery(accessToken, token.RefreshToken);
        Result<Response.TokenResponse> result = await sender.Send(refreshToken);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }

    private  static async Task<IResult> RefreshToken(ISender sender)
    {
        var revokeToken = new RevokeTokenCommand();
        Result result = await sender.Send(revokeToken);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }
}
