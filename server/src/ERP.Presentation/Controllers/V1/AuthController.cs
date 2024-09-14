using Asp.Versioning;
using ERP.Presentation.Abstractions;
using ERP.Application.UseCases.Indentity.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.V1;
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

   
}
