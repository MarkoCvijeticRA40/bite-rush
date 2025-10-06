using System.Reflection;
using Application;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Serilog;
using Stripe;
using Web.Api;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddCors(options =>
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()));

if (builder.Environment.IsDevelopment())
{
    IConfigurationSection keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
    IConfigurationSection keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
    IConfigurationSection keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    IConfigurationSection keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");

    var credential = new ClientSecretCredential(keyVaultDirectoryID.ToString(), keyVaultClientId.ToString(), keyVaultClientSecret.ToString());

    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value, keyVaultClientId.Value, keyVaultClientSecret.Value, new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);
}

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

WebApplication app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCors("AllowReactApp");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

app.Use(async (context, next) =>
{
    Console.WriteLine($"?? {context.Request.Method} {context.Request.Path}");
    await next();
});

// REMARK: Required for functional and integration tests to work.
namespace Web.Api
{
    public partial class Program;
}
