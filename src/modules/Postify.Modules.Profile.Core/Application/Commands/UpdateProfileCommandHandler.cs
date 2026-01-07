using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Application.Commands;

public class UpdateProfileCommandHandler
{
    private readonly IProfileDbContext _profileDbContext;

    public UpdateProfileCommandHandler(IProfileDbContext profileDbContext)
    {
        _profileDbContext = profileDbContext;
    }

    public async Task<ProfileResponse> HandleAsync(UpdateProfileCommand command)
    {
        var profile = await _profileDbContext.Profiles.FindAsync(command.ProfileId);
        if (profile == null)
        {
            throw new ServiceErrorException(
                Error.NotFound(
                    title: "پروفایل یافت نشد",
                    description: $"پروفایلی با شناسه {command.ProfileId} یافت نشد.",
                    code: "profile.not_found"
                )
            );
        }

        if (profile is IndividualProfile individualProfile)
        {
            if (!string.IsNullOrWhiteSpace(command.FirstName))
                individualProfile.FirstName = command.FirstName;
            if (!string.IsNullOrWhiteSpace(command.LastName))
                individualProfile.LastName = command.LastName;
            if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
                individualProfile.PhoneNumber = command.PhoneNumber;
        }
        else if (profile is CorporateProfile corporateProfile)
        {
            if (!string.IsNullOrWhiteSpace(command.Name))
                corporateProfile.Name = command.Name;
            if (!string.IsNullOrWhiteSpace(command.EconomicId))
            {
                var existingCorporateProfile = await _profileDbContext.CorporateProfiles
                    .FirstOrDefaultAsync(p => p.EconomicId == command.EconomicId && p.Id != command.ProfileId);

                if (existingCorporateProfile != null)
                {
                    throw new ServiceErrorException(
                        Error.Failure(
                            title: "شناسه اقتصادی تکراری",
                            description: $"پروفایل شرکتی با شناسه اقتصادی {command.EconomicId} از قبل وجود دارد.",
                            code: "profile.economic_id_already_exists"
                        )
                    );
                }
                corporateProfile.EconomicId = command.EconomicId;
            }
        }

        await _profileDbContext.SaveChangesAsync();

        return new ProfileResponse
        {
            Id = profile.Id,
            NationalId = profile.NationalId,
            ProfileType = profile is IndividualProfile ? "Individual" : "Corporate"
        };
    }
}

