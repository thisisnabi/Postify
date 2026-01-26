using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Shortak.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddShortakCore(this IServiceCollection services)
    {
        return services;
    }
}
