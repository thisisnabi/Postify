using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Notify;

public static class ModuleExtensions
{
    public static IServiceCollection AddNotifyModule(this IServiceCollection services, IConfiguration configuration)
    {
 
        return services;
    }
}
