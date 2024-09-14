using ERP.Share.Abstractions.Shared;
using ERP.Share.Responses.Token;

namespace ERP.Application.Abstractions.Authentication;
public interface IAuthenticationService
{
    public Task<Result<TokenResponse>> GenerateToken(string username, string password, CancellationToken cancellationToken = default);
}
