using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postify.Modules.Profile.Core.Extensions;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Modules.Profile.Infrastructure.Extensions;
using Postify.Modules.Profile.Validators;

namespace Postify.Modules.Profile;

public static class ModuleExtensions
{
    public static IServiceCollection AddProfileModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProfileCore()
                .AddProfileInfrastructure(configuration);

        services.AddScoped<IValidator<RegisterIndividualProfileRequest>, RegisterIndividualProfileRequestValidator>();
        services.AddScoped<IValidator<RegisterCorporateProfileRequest>, RegisterCorporateProfileRequestValidator>();
        services.AddScoped<IValidator<UpdateProfileRequest>, UpdateProfileRequestValidator>();
        services.AddScoped<RegisterIndividualProfileRequestValidator>();
        services.AddScoped<RegisterCorporateProfileRequestValidator>();
        services.AddScoped<UpdateProfileRequestValidator>();

        return services;
    }
}
