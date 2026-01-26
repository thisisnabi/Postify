using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Media.Core.Extensions;
using Postify.Modules.Media.Infrastructure.Extensions;

namespace Postify.Modules.Media;

public static class ModuleExtensions
{
    public static IServiceCollection AddMediaModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediaCore()
                .AddMediaInfrastructure(configuration);

        return services;
    }
}
