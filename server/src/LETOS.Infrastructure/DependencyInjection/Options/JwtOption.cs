namespace LETOS.Infrastructure.DependencyInjection.Options;
public sealed class JwtOption
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpireMin { get; set; }
}
