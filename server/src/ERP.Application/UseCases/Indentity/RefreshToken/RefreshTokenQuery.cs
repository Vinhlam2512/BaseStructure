using ERP.Share.Abstractions.Shared;
using ERP.Share.Responses.Token;

namespace ERP.Application.UseCases.Indentity.RefreshToken;

public record RefreshTokenQuery(string? AccessToken, string? RefreshToken) : IQuery<TokenResponse>;
