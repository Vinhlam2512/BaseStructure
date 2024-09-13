using ERP.Domain.Exceptions;

namespace ERP.Domain.Entities.Users;

public static class UserException
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId)
            : base($"Người dùng với ID {userId} không tồn tại!") { }
    }

    public class InvalidCredentialsException : BadRequestException
    {
        public InvalidCredentialsException(string message)
            : base(message) { }
    }

    public class InvalidAccountException : NotFoundException
    {
        public InvalidAccountException()
            : base("Tài khoản hoặc mật khẩu không chính xác!") { }
    }
}
