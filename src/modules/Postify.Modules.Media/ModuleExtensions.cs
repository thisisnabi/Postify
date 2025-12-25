using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Media;

public static class ModuleExtensions
{
    public static IServiceCollection AddMediaModule(this IServiceCollection services, IConfiguration configuration)
    {
 
        return services;
    }
}
