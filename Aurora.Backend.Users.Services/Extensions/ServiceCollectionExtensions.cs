using Aurora.Backend.Users.Services.Contracts;
using Aurora.Backend.Users.Services.Implements;
using Aurora.Backend.Users.Services.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Backend.Users.Services.Extensions
{
	public static class ServiceCollectionExtensions
	{
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuroraContext>((serviceProvider, options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DB"),
                    npgsqlOptions => 
                    {
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                    });
            });
            
            return services;
        }

    }
}

