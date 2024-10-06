namespace LETOS.Domain.Exceptions;
public class IdentityException
{
    public class TokenException : BadRequestException
    {
        public TokenException(string message)
            : base("Token Exception", message)
        {
        }
    }
}
