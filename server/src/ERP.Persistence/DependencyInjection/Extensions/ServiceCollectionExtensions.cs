using ERP.Domain.Abstractions;
using ERP.Domain.Abstractions.Repositories;
using ERP.Persistence.DependencyInjection.Options;
using ERP.Persistence.Interceptors;
using ERP.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ERP.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AddSqlServer(services);

        AddInterceptorDbContext(services);

        AddRepositoryBaseConfiguration(services);

        ConfigureSqlServerRetryOptions(services, configuration.GetSection(nameof(SqlServerRetryOptions)));

        return services;
    }

    public static void AddSqlServer(IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {



            // Interceptor
            var outboxInterceptor = provider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            var auditableInterceptor = provider.GetService<UpdateAuditableEntitiesInterceptor>();
            var sofeDeleteInterceptor = provider.GetService<SoftDeleteInterceptor>();
            var userTrackingInterceptor = provider.GetService<UserTrackingInterceptor>();



            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();


            //builder
            //.EnableDetailedErrors(true)
            //.EnableSensitiveDataLogging(true)
            //.UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            //.UseSqlServer(
            //    connectionString: configuration.GetConnectionString("DefaultConnection"),
            //    sqlServerOptionsAction: optionsBuilder
            //            => optionsBuilder.ExecutionStrategy(
            //                    dependencies => new SqlServerRetryingExecutionStrategy(
            //                        dependencies: dependencies,
            //                        maxRetryCount: options.CurrentValue.MaxRetryCount,
            //                        maxRetryDelay: options.CurrentValue.MaxRetryDelay,
            //                        errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
            //                .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name))
            //.AddInterceptors(outboxInterceptor, auditableInterceptor);


            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            //.UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name))
             .AddInterceptors(
                    outboxInterceptor
                    , sofeDeleteInterceptor
                    , auditableInterceptor
                    , userTrackingInterceptor);

        });

    }

    public static void AddInterceptorDbContext(IServiceCollection services)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddSingleton<UserTrackingInterceptor>();
    }

    public static void AddRepositoryBaseConfiguration(IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
        services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
    }




    public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions(IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<SqlServerRetryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
