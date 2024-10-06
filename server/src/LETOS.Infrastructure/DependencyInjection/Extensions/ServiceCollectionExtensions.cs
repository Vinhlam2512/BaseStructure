using System.Text;
using LETOS.Application.Abstractions.Authentication;
using LETOS.Application.Abstractions.Caching;
using LETOS.Infrastructure.Authentication;
using LETOS.Infrastructure.Authorization;
using LETOS.Infrastructure.Caching;
using LETOS.Infrastructure.DependencyInjection.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AuthenticationService = LETOS.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = LETOS.Application.Abstractions.Authentication.IAuthenticationService;

namespace LETOS.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        AddRedisCache(services, configuration);


        services.AddScoped<IUserContext, UserContext>();

        // AddMasstransitRabbitMQInfrastructure(services, configuration);

        AddJwtAuthenticationAPI(services, configuration);

        AddAuthorization(services);


        // AddQuartzInfrastructure(services);

        return services;
    }

    public static void AddJwtAuthenticationAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            JwtOption jwtOption = new JwtOption();
            configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

            o.SaveToken = true; // Save token into AuthenticationProperties

            var Key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, // on production make it true
                ValidateAudience = true, // on production make it true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOption.Issuer,
                ValidAudience = jwtOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                    }
                    return Task.CompletedTask;
                }
            };

            o.EventsType = typeof(CustomJwtBearerEvents);
        });


        services.AddAuthorization();
        services.AddScoped<CustomJwtBearerEvents>();



        services.Configure<JwtOption>(configuration.GetSection(nameof(JwtOption)));

        services.AddTransient<IJwtService, JwtService>();
        services.AddTransient<IPasswordService, PasswordService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddScoped<AuthorizationService>();
        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
    }


    public static void AddRedisCache(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Redis") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    // public static void AddMasstransitRabbitMQInfrastructure(IServiceCollection services, IConfiguration configuration)
    // {
    //     var masstransitConfiguration = new MasstransitConfiguration();
    //     configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
    //
    //     var messageBusOption = new MessageBusOptions();
    //     configuration.GetSection(nameof(MessageBusOptions)).Bind(messageBusOption);
    //
    //     services.AddMassTransit(cfg =>
    //     {
    //         // ===================== Setup for Consumer =====================
    //         cfg.AddConsumers(Assembly
    //             .GetExecutingAssembly()); // Add all of consumers to masstransit instead above command
    //
    //         // ?? => Configure endpoint formatter. Not configure for producer Root Exchange
    //         cfg.SetKebabCaseEndpointNameFormatter(); // ??
    //
    //         cfg.UsingRabbitMq((context, bus) =>
    //         {
    //             bus.Host(masstransitConfiguration.Host, masstransitConfiguration.Port, masstransitConfiguration.VHost,
    //                 h =>
    //                 {
    //                     h.Username(masstransitConfiguration.UserName);
    //                     h.Password(masstransitConfiguration.Password);
    //                 });
    //
    //             bus.UseMessageRetry(retry
    //                 => retry.Incremental(
    //                     retryLimit: messageBusOption.RetryLimit,
    //                     initialInterval: messageBusOption.InitialInterval,
    //                     intervalIncrement: messageBusOption.IntervalIncrement));
    //
    //             bus.UseNewtonsoftJsonSerializer();
    //
    //             bus.ConfigureNewtonsoftJsonSerializer(settings =>
    //             {
    //                 settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
    //                 settings.Converters.Add(new DateOnlyJsonConverter());
    //                 settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
    //                 return settings;
    //             });
    //
    //             bus.ConfigureNewtonsoftJsonDeserializer(settings =>
    //             {
    //                 settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
    //                 settings.Converters.Add(new DateOnlyJsonConverter());
    //                 settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
    //                 return settings;
    //             });
    //
    //             bus.ConnectReceiveObserver(new LoggingReceiveObserver());
    //             bus.ConnectConsumeObserver(new LoggingConsumeObserver());
    //             bus.ConnectPublishObserver(new LoggingPublishObserver());
    //             bus.ConnectSendObserver(new LoggingSendObserver());
    //
    //             // Rename for Root Exchange and setup for consumer also
    //             bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
    //
    //             // ===================== Setup for Consumer =====================
    //
    //             // Importantce to create Echange and Queue
    //             bus.ConfigureEndpoints(context);
    //         });
    //     });
    // }
    //
    // // Configure Job
    // public static void AddQuartzInfrastructure(IServiceCollection services)
    // {
    //     services.AddQuartz(configure =>
    //     {
    //         var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
    //
    //         configure
    //             .AddJob<ProcessOutboxMessagesJob>(jobKey)
    //             .AddTrigger(
    //                 trigger =>
    //                     trigger.ForJob(jobKey)
    //                         .WithSimpleSchedule(
    //                             schedule =>
    //                                 schedule.WithInterval(TimeSpan.FromMicroseconds(100))
    //                                     .RepeatForever()));
    //
    //         configure.UseMicrosoftDependencyInjectionJobFactory();
    //     });
    //
    //     services.AddQuartzHostedService();
    // }
}


