using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Proof.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProofCore(this IServiceCollection services)
    {
        return services;
    }
}
