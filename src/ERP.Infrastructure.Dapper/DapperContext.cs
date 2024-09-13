using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ERP.Persistence;
public class DapperContext
{
    private readonly IConfiguration _configuration;

    private readonly string connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection Connection => new SqlConnection(connectionString);
}
