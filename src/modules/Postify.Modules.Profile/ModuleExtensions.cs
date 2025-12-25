using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Profile.Core.Extensions;
using Postify.Modules.Profile.Infrastructure.Extensions;

namespace Postify.Modules.Profile;

public static class ModuleExtensions
{
    public static IServiceCollection AddProfileModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProfileCore()
                .AddProfileInfrastructure(configuration);

        return services;
    }
}
