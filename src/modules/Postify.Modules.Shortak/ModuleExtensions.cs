using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Shortak.Core.Extensions;
using Postify.Modules.Shortak.Infrastructure.Extensions;

namespace Postify.Modules.Shortak;

public static class ModuleExtensions
{
    public static IServiceCollection AddShortakModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddShortakCore()
                .AddShortakInfrastructure(configuration);

        return services;
    }
}
