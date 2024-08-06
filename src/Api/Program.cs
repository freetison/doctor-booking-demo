using Api.DependencyInjection;

using FastEndpoints;
using FastEndpoints.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Prepare all the configuration data
var config = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

// Add services needed 
builder.Services
    .AddSecurityServices()
    .AddCommonServices(config)
    .AddHttpServices(config)
    .AddAppServices();


var app = builder.Build();

app.UseCors("AllowLocalhost")
    .UseDefaultExceptionHandler()
    .UseFastEndpoints(x => x.Errors.UseProblemDetails());

// Usually only for dev, but this is a demo. 
// We want to see it swagger to test
app.UseSwaggerGen();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //.UseSwaggerGen();
}

await app.RunAsync();




