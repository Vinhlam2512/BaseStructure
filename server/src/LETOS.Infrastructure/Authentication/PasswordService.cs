using System.Security.Cryptography;
using System.Text;
using LETOS.Application.Abstractions.Authentication;

namespace LETOS.Infrastructure.Authentication;
public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] originalBytes = Encoding.Default.GetBytes(password);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }

    public bool VerifyHashedPassword(string passwordInput, string password)
    {
        string hashedProvidedPassword = HashPassword(passwordInput);
        if (hashedProvidedPassword == password)
        {
            return true;
        }
        return false;
    }
}
