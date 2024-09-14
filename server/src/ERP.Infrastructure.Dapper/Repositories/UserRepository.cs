using System.Data;
using Dapper;
using ERP.Share.Responses.Users;
using ERP.Domain.Entities.Users;
using ERP.Persistence;
using ERP.Persistence;
using ERP.Persistence.Repositories;

namespace ERP.Infrastructure.Dapper.Repositories;
public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
{
    private readonly DapperContext _dapperContext;

    public UserRepository(ApplicationDbContext context,DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    public async Task<GetUserResponse> GetUserById(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("id", id);

        using IDbConnection db = _dapperContext.Connection;

        GetUserResponse user = await db.QuerySingleAsync<GetUserResponse>("GetUserById", parameters, commandType: CommandType.StoredProcedure);

        return user;
    }
}
