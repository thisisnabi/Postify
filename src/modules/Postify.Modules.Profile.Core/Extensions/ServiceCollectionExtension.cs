using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Profile.Core.Application.Commands;

namespace Postify.Modules.Profile.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProfileCore(this IServiceCollection services)
    {
        services.AddScoped<RegisterIndividualProfileCommandHandler>();
        services.AddScoped<RegisterCorporateProfileCommandHandler>();
        services.AddScoped<UpdateProfileCommandHandler>();

        return services;
    }
}
