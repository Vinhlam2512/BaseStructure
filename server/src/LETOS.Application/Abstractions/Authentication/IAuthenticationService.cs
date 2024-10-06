using LETOS.Share.Abstractions.Shared;
using LETOS.Share.Responses.Token;

namespace LETOS.Application.Abstractions.Authentication;
public interface IAuthenticationService
{
    public Task<Result<TokenResponse>> GenerateToken(string username, string password, CancellationToken cancellationToken = default);
}
