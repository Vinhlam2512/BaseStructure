using ERP.Domain.Entities.Users;
using ERP.Infrastructure.Dapper.Repositories;
using ERP.Persistence;
using ERP.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Infrastructure.Dapper.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDapper(this IServiceCollection services)
    {
        AddDapper(services);

        AddRepository(services);

        return services;
    }

    public static void AddRepository(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
    }


    public static void AddDapper(IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();
    }
}
