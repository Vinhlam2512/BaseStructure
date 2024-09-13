using System.Reflection;
using FluentValidation;
using ERP.Application.Behaviors;
using ERP.Application.Mapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddMediatR(services: services);
        AddAutoMapperApplication(services: services);
        return services;
    }

    public static void AddMediatR(this IServiceCollection services)
        => services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryCachingBehavior<,>))
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
        .AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

    public static void AddAutoMapperApplication(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());

}
