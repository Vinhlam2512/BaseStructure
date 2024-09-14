using ERP.Domain.Abstractions.Repositories;

namespace ERP.Domain.Entities.Users;
public interface IUserRepository : IRepositoryBase<User, Guid>
{

}
