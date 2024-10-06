using System.Security.Claims;
using LETOS.Application.Abstractions.Authentication;
using LETOS.Application.Abstractions.Caching;
using LETOS.Domain.Entities.Users;
using LETOS.Infrastructure.DependencyInjection.Options;
using LETOS.Share.Abstractions.Shared;
using LETOS.Share.Responses.Token;
using Microsoft.Extensions.Options;
using IAuthenticationService = LETOS.Application.Abstractions.Authentication.IAuthenticationService;

namespace LETOS.Infrastructure.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private static readonly Error ErrorUserOrPass = new Error("LoginFailed", "Tài khoản hoặc mật khẩu không chính xác!");
    private static readonly Error AccountLock = new Error("AccountLocked", "Tài khoản của bạn bị khóa do đăng nhập sai nhiều lần!");


    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ICacheService _cacheService;

    private readonly JwtOption _jwtOption;

    public AuthenticationService(
            IJwtService jwtService,
            IUserRepository userRepository,
            IPasswordService passwordService,
            ICacheService cacheService,
            IOptions<JwtOption> jwtOption
        )
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _cacheService = cacheService;

        _jwtOption = jwtOption.Value;
    }

    public async Task<Result<TokenResponse>> GenerateToken(string username, string password, CancellationToken cancellationToken = default)
    {
        User? checkUser = await _userRepository.FindSingleAsync(x => x.Name.UserName == username);
        if (checkUser == null)
        {
            return Result.Failure<TokenResponse>(ErrorUserOrPass);
        }
        else
        {
            string passwordUser = checkUser.PassWordHashed;
            var checkPass = _passwordService.VerifyHashedPassword(password, passwordUser);
            if (!checkPass)
            {
                checkUser.CountAccessFailed();
                return Result.Failure<TokenResponse>(ErrorUserOrPass);
            }

            if (checkUser.IsLocked)
            {
                return Result.Failure<TokenResponse>(AccountLock);
            }

            checkUser.ResetAccessFailed();

            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, checkUser.Name.UserName),
                 new Claim(ClaimTypes.Sid, checkUser.Id.ToString()),
             };

            string accessToken = _jwtService.GenerateAccessToken(claims);
            string refreshToken = _jwtService.GenerateRefreshToken();

            TokenResponse response = new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_jwtOption.ExpireMin)
            };

            await _cacheService.SetAsync(checkUser.Id.ToString(), response, TimeSpan.FromMinutes(_jwtOption.ExpireMin), cancellationToken);

            return Result.Success(response);
        }
    }
}
