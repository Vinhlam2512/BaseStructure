using LETOS.Share.Abstractions.Shared;
using LETOS.Share.Responses.Token;

namespace LETOS.Application.UseCases.Indentity.RefreshToken;

public record RefreshTokenQuery(string? AccessToken, string? RefreshToken) : IQuery<TokenResponse>;
