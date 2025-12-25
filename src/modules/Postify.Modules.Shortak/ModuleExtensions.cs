using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Shortak;

public static class ModuleExtensions
{
    public static IServiceCollection AddShortakModule(this IServiceCollection services, IConfiguration configuration)
    {
 
        return services;
    }
}
