namespace ERP.Application.Abstractions.Authentication;
public interface IPasswordService
{
    public string HashPassword(string password);

    public bool VerifyHashedPassword(string passwordInput, string password);
}
