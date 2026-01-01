using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Proof.Infrastructure.Persistence;

namespace Postify.Modules.Proof.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProofInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProofDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Proof");
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}
