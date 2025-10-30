using System.Reflection;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;
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
    /*IConfigurationSection keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
    IConfigurationSection keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
    IConfigurationSection keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    IConfigurationSection keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");

    var credential = new ClientSecretCredential(keyVaultDirectoryID.ToString(), keyVaultClientId.ToString(), keyVaultClientSecret.ToString());

    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value, keyVaultClientId.Value, keyVaultClientSecret.Value, new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);*/
}

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddQueueServiceClient(builder.Configuration.GetConnectionString("azurefunctions"));
});

// Use AddMicrosoftIdentityWebApi for APIs to validate Bearer tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

// 6. Authorization: Checks if the identified user has permission
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UsersAccessPermission", policy =>
    {
        policy.RequireScope("Users.Read");
    });
});

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

WebApplication app = builder.Build();

// --- MIDDLEWARE PIPELINE ---

// 1. Exception handling and logging should be first
app.UseExceptionHandler();
app.UseRequestContextLogging();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
}

// 2. HTTPS Redirection (recommended, add if needed)
// app.UseHttpsRedirection();

// 3. CORS must be before Authentication/Authorization
app.UseCors("AllowReactApp");

// 4. Custom logging middleware (moved from end of file)
app.Use(async (context, next) =>
{
    Console.WriteLine($"?? {context.Request.Method} {context.Request.Path}");
    await next();
});

// 5. Authentication: Identifies who the user is
app.UseAuthentication();

// 7. Endpoint Mapping: Map all endpoints AFTER all middleware is configured
app.MapEndpoints();
app.MapControllers();
app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// 8. Run the application (must be last)
await app.RunAsync();
