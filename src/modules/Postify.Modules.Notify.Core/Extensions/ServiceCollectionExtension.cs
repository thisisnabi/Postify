using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Notify.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddNotifyCore(this IServiceCollection services)
    {
        return services;
    }
}
