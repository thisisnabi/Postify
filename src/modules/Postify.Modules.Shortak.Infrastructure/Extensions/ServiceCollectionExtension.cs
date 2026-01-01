using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Shortak.Infrastructure.Persistence;

namespace Postify.Modules.Shortak.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddShortakInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShortakDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Shortak");
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}
