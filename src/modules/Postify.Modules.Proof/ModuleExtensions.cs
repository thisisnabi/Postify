using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Proof.Core.Extensions;
using Postify.Modules.Proof.Infrastructure.Extensions;

namespace Postify.Modules.Proof;

public static class ModuleExtensions
{
    public static IServiceCollection AddProofModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProofCore()
                .AddProofInfrastructure(configuration);

        return services;
    }
}
