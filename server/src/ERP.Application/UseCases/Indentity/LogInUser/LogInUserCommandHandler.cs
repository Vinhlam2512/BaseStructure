using ERP.Application.Abstractions.Authentication;
using ERP.Share.Abstractions.Shared;
using ERP.Share.Responses.Token;
namespace ERP.Application.UseCases.Indentity.Login;
public class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, LogInUserResponse>
{
    private readonly IAuthenticationService _authenService;

    public LogInUserCommandHandler(IAuthenticationService authenService)
    {
        _authenService = authenService;
    }

    public async Task<Result<LogInUserResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        string username = request.UserName;
        string passwordInput = request.Password;

        Result<TokenResponse> token = await _authenService.GenerateToken(username, passwordInput, cancellationToken);
        if (token.IsFailure)
        {
            return Result.Failure<LogInUserResponse>(token.Error);
        }

        LogInUserResponse response = new LogInUserResponse()
        {
            AccessToken = token.Value.AccessToken,
            RefreshToken = token.Value.RefreshToken,
            RefreshTokenExpiryTime = token.Value.RefreshTokenExpiryTime
        };

        return Result.Success(response);
    }

}

//private string GetClientIpAddress()
//{
//    IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;
//    if (headers.ContainsKey("X-Forwarded-For"))
//    {
//        return headers["X-Forwarded-For"].FirstOrDefault();
//    }

//    if (headers.ContainsKey("X-Real-IP"))
//    {
//        return headers["X-Real-IP"].FirstOrDefault();
//    }

//    return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
//}


