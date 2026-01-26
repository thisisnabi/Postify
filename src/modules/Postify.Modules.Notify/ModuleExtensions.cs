using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Notify.Core.Extensions;
using Postify.Modules.Notify.Infrastructure.Extensions;

namespace Postify.Modules.Notify;

public static class ModuleExtensions
{
    public static IServiceCollection AddNotifyModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNotifyCore()
                .AddSmsInfrastructure(configuration);

        return services;
    }
}
