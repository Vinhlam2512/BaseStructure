using Asp.Versioning;
using ERP.Api.Abstractions;
using ERP.Application.UseCases.Users.CreateUser;
using ERP.Share.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers.V1;
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/khach_hang")]
public class KhachHangController : ApiController
{
    public KhachHangController(ISender sender) : base(sender)
    {
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateKhachHang([FromBody] CreateUserCommand command)
    {
        Result result = await Sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Ok(result);
    }
}
