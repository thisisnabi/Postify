using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Application.Commands;

public class RegisterIndividualProfileCommandHandler
{
    private readonly IProfileDbContext _profileDbContext;

    public RegisterIndividualProfileCommandHandler(IProfileDbContext profileDbContext)
    {
        _profileDbContext = profileDbContext;
    }

    public async Task<ProfileResponse> HandleAsync(RegisterIndividualProfileCommand command)
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

        var profile = new IndividualProfile
        {
            NationalId = command.NationalId,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PhoneNumber = command.PhoneNumber
        };

        _profileDbContext.IndividualProfiles.Add(profile);
        await _profileDbContext.SaveChangesAsync();

        return new ProfileResponse
        {
            Id = profile.Id,
            NationalId = profile.NationalId,
            ProfileType = "Individual"
        };
    }
}

