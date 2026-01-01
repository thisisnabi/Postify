using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Notify.Infrastructure.Extensions;

namespace Postify.Modules.Notify;

public static class ModuleExtensions
{
    public static IServiceCollection AddNotifyModule(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddProfileCore()
        services.AddSmsInfrastructure(configuration);

        return services;
    }
}
