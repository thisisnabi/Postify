using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Media.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMediaCore(this IServiceCollection services)
    {
        return services;
    }
}
