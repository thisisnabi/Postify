using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Application.Commands;

public class RegisterCorporateProfileCommandHandler
{
    private readonly IProfileDbContext _profileDbContext;

    public RegisterCorporateProfileCommandHandler(IProfileDbContext profileDbContext)
    {
        _profileDbContext = profileDbContext;
    }

    public async Task<ProfileResponse> HandleAsync(RegisterCorporateProfileCommand command)
    {
        var existingProfile = await _profileDbContext.Profiles
            .FirstOrDefaultAsync(p => p.NationalId == command.NationalId);

        if (existingProfile != null)
        {
            throw new ServiceErrorException(
                Error.Failure(
                    title: "پروفایل تکراری",
                    description: $"پروفایلی با کد ملی {command.NationalId} از قبل وجود دارد.",
                    code: "profile.national_id_already_exists"
                )
            );
        }

        var existingCorporateProfile = await _profileDbContext.CorporateProfiles
            .FirstOrDefaultAsync(p => p.EconomicId == command.EconomicId);

        if (existingCorporateProfile != null)
        {
            throw new ServiceErrorException(
                Error.Failure(
                    title: "پروفایل شرکتی تکراری",
                    description: $"پروفایل شرکتی با شناسه اقتصادی {command.EconomicId} از قبل وجود دارد.",
                    code: "profile.economic_id_already_exists"
                )
            );
        }

        var profile = new CorporateProfile
        {
            NationalId = command.NationalId,
            Name = command.Name,
            EconomicId = command.EconomicId
        };

        _profileDbContext.CorporateProfiles.Add(profile);
        await _profileDbContext.SaveChangesAsync();

        return new ProfileResponse
        {
            Id = profile.Id,
            NationalId = profile.NationalId,
            ProfileType = "Corporate"
        };
    }
}

