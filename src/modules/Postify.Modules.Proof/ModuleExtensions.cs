using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Postify.Modules.Proof;

public static class ModuleExtensions
{
    public static IServiceCollection AddProofModule(this IServiceCollection services, IConfiguration configuration)
    {
 
        return services;
    }
}
