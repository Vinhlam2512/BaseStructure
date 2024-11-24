using Asp.Versioning;
using LETOS.Api.Abstractions;
using LETOS.Application.UseCases.Users.CreateUser;
using LETOS.Share.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LETOS.Api.Controllers.V1;
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

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetShops([FromQuery] GetShopsQuery query)
    //{
    //    var result = await Sender.Send(query);
    //    return result.IsFailure ? HandlerFailure(result) : Ok(result.Value);
    //}
}
