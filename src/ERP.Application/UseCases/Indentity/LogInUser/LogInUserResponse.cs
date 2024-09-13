namespace ERP.Application.UseCases.Indentity.Login;

public sealed class LogInUserResponse
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }
}
