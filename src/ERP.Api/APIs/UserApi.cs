using Carter;
using ERP.Api.Controllers;
using ERP.Application.UseCases.Users.CreateUser;
using ERP.Application.UseCases.Users.GetUser;
using ERP.Application.UseCases.Users.LogOutUser;
using ERP.Contract.Abstractions.Shared;
using ERP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.APIs;

public class UserApi : ApiEndpoint, ICarterModule
{

    private const string BaseUrl = "api/v{version:apiVersion}/user";
    private const string Name = "Người dùng";
    private const int ApiVersion = ApiVersions.V1;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var userV1 = app.NewVersionedApi(Name)
            .MapGroup(BaseUrl).HasApiVersion(ApiVersion).RequireAuthorization();

        userV1.MapPost(string.Empty, CreateUser).AllowAnonymous();
        userV1.MapGet("get_user", GetUser)
            .RequireAuthorization(new AuthorizeAttribute { Roles = Roles.Registered });
        //.RequirePermissions(Permissions.Write, Permissions.Read);
        userV1.MapGet("logout", LogOutUser);
    }

    private static async Task<IResult> LogOutUser(ISender sender, [FromBody] LogOutUserCommand command)
    {
        Result result = await sender.Send(command);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }


    private static async Task<IResult> CreateUser(ISender sender, [FromBody] CreateUserCommand command)
    {
        Result result = await sender.Send(command);

        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }



    private static async Task<IResult> GetUser(ISender sender)
    {
        var getUser = new GetUserQuery();
        Result result = await sender.Send(getUser);

        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }
}
