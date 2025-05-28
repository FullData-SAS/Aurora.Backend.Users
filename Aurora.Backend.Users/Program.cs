using Aurora.Backend.Users.Services.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var version = "v1.0.0.0";
var appName = "Users";

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = version,
        Title = $"{appName} API",
        Description = $"An ASP.NET Core Web API for {appName} by Id Factory",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddHttpClient();
builder.Services.AddServices();
builder.Services.AddDataBase(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddHealthChecks();

var app = builder.Build();
// app.Urls.Add("http://0.0.0.0:5001");
app.UseHealthChecks($"/{appName}/HealthCheck", new HealthCheckOptions()
{
    // The following StatusCodes are the default assignments for the HealthStatus properties.
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSwagger(c =>
{
    c.RouteTemplate = $"{appName}/swagger/{{documentname}}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/{appName}/swagger/v1/swagger.json", $"{appName} API {version}");
    c.RoutePrefix = $"{appName}/swagger";
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(builder => builder.WithOrigins("*")
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseAuthorization();
app.MapControllers();

app.Run();