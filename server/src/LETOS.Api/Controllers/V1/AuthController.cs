using Asp.Versioning;
using LETOS.Api.Abstractions;
using LETOS.Application.UseCases.Indentity.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LETOS.Api.Controllers.V1;
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LogInUserCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Ok(result);
    }
}
