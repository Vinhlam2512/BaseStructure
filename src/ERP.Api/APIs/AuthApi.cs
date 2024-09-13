using Carter;
using ERP.Application.UseCases.Indentity.Login;
using ERP.Contract.Abstractions.Shared;
using ERP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.APIs;

public class AuthApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "api/v{version:apiVersion}/auth";
    private const string Name = "Authencation";
    private const int ApiVersion = ApiVersions.V1;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder authV1 = app.NewVersionedApi(Name)
            .MapGroup(BaseUrl).HasApiVersion(ApiVersion);

        authV1.MapPost("login", Login);
    }

    private async Task<IResult> Login(ISender sender, [FromBody] LogInUserCommand login)
    {
        Result result = await sender.Send(login);

        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }
}
