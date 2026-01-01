using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Notify.Core.Abstractions;
using Postify.Modules.Notify.Infrastructure.Persistence;
using Postify.Modules.Notify.Infrastructure.Providers;

namespace Postify.Modules.Notify.Infrastructure.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSmsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SmsDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Sms");
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ISmsProvider, KavehNegarProvider>();

        return services;
    }
}
