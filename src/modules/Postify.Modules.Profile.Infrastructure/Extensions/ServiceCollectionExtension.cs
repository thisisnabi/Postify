using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Profile.Infrastructure.Persistence;

namespace Postify.Modules.Profile.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProfileInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfileDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Profile");
            options.UseSqlServer(connectionString);
        });
         
        return services;
    }
}
