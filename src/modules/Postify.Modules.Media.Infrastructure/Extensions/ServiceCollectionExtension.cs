using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Media.Core.Abstractions;
using Postify.Modules.Media.Infrastructure.Persistence;
using Postify.Modules.Media.Infrastructure.Providers;

namespace Postify.Modules.Media.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMediaInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MediaDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Media");
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IObjectStorageProvider, S3AmazonProvider>();
        return services;
    }
}
