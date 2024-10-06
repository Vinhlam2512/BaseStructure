﻿using Asp.Versioning;
using LETOS.Api.Abstractions;
using LETOS.Application.UseCases.Users.CreateUser;
using LETOS.Application.UseCases.Users.GetUser;
using LETOS.Application.UseCases.Users.LogOutUser;
using LETOS.Share.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LETOS.Api.Controllers.V1;
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/user")]
[Authorize]
public class UserController : ApiController
{
    public UserController(ISender sender)
        : base(sender)
    {
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        Result result = await Sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Ok(result);
    }




    [HttpGet("get_user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[HasRole(Roles.Registered)]
    //[HasPermission(Permissions.Read)]
    public async Task<IActionResult> GetUserId()
    {
        var getUser = new GetUserQuery();
        var result = await Sender.Send(getUser);
        return result.IsFailure ? HandlerFailure(result) : Ok(result);
    }


    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LogOutUser([FromBody] LogOutUserCommand getUser)
    {
        Result result = await Sender.Send(getUser);
        return result.IsFailure ? HandlerFailure(result) : Ok(result);
    }

}
