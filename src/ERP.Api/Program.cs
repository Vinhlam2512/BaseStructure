using ERP.Api.DependencyInjection.Extensions;
using ERP.Api.Middleware;
using ERP.Application.DependencyInjection.Extensions;
using ERP.Infrastructure.Dapper.DependencyInjection.Extensions;
using ERP.Infrastructure.DependencyInjection.Extensions;
using ERP.Persistence.DependencyInjection.Extensions;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add configuration

Log.Logger = new LoggerConfiguration().ReadFrom
                                    .Configuration(builder.Configuration).WriteTo
                                    .Console()
                                    .CreateLogger();


builder.Logging
    .ClearProviders()
    .AddSerilog();


builder.Host.UseSerilog();


builder.Services.AddInfrastructure(builder.Configuration);


//builder.Services.AddServicesInfrastructureErp();


builder.Services.AddApplication();


builder
    .Services
    .AddControllers();
// .AddJsonOptions(options =>
//  {
//      options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
//  })
// .AddNewtonsoftJson(options =>
// {
//     options.SerializerSettings.ShareResolver = new DefaultShareResolver
//     {
//         NamingStrategy = new SnakeCaseNamingStrategy()
//     };
// });

builder.Services.AddTransient<ExceptionHandlingMiddleware>();


// builder.Services.AddCarter();
//builder.Services.AddCarterWithAssemblies(typeof(Program).Assembly);

// Configure Dapper
builder.Services.AddInfrastructureDapper();

builder.Services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwagger();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// builder.Services.AddJwtAuthenticationAPI(builder.Configuration);


// AddPersistence
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


// Add API Endpoint with carter module
//app.MapCarter();

//app.UseHttpsRedirection();


app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true)
          .AllowCredentials());


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
app.ConfigureSwagger();
app.UseStaticFiles();
try
{
    Console.WriteLine("Run app");
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

public partial class Program { }
