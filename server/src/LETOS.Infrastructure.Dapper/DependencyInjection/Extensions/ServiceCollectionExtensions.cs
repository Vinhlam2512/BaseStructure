using LETOS.Domain.Entities.Users;
using LETOS.Infrastructure.Dapper.Repositories;
using LETOS.Persistence;
using LETOS.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace LETOS.Infrastructure.Dapper.DependencyInjection.Extensions;
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
