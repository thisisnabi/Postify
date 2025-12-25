using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Common.Helpers;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Modules.Profile.Core.Payloads.Validators;

namespace Postify.Modules.Profile.Core.Application;

public class ProfileService
{
    private readonly IProfileDbContext _profileDbContext;

    public ProfileService(IProfileDbContext profileDbContext)
    {
        _profileDbContext = profileDbContext;
    }

    public async Task<ProfileResponse> RegisterIndividualProfileAsync(RegisterIndividualProfileRequest request)
    {
        var validator = new RegisterIndividualProfileRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid) throw ExceptionHelper.BadRequest(result);

        var existingProfile = await _profileDbContext.Profiles
            .FirstOrDefaultAsync(p => p.NationalId == request.NationalId);

        if (existingProfile != null)
        {
            throw new InvalidOperationException($"Profile with NationalId {request.NationalId} already exists.");
        }

        var profile = new IndividualProfile
        {
            NationalId = request.NationalId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
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

    public async Task<ProfileResponse> RegisterCorporateProfileAsync(RegisterCorporateProfileRequest request)
    {
        var validator = new RegisterCorporateProfileRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid) throw ExceptionHelper.BadRequest(result);

        var existingProfile = await _profileDbContext.Profiles
            .FirstOrDefaultAsync(p => p.NationalId == request.NationalId);

        if (existingProfile != null)
        {
            throw new InvalidOperationException($"Profile with NationalId {request.NationalId} already exists.");
        }

        var existingCorporateProfile = await _profileDbContext.CorporateProfiles
            .FirstOrDefaultAsync(p => p.EconomicId == request.EconomicId);

        if (existingCorporateProfile != null)
        {
            throw new InvalidOperationException($"Corporate profile with EconomicId {request.EconomicId} already exists.");
        }

        var profile = new CorporateProfile
        {
            NationalId = request.NationalId,
            Name = request.Name,
            EconomicId = request.EconomicId
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

    public async Task<ProfileResponse> UpdateProfileAsync(long profileId, UpdateProfileRequest request)
    {
        var validator = new UpdateProfileRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid) throw ExceptionHelper.BadRequest(result);

        var profile = await _profileDbContext.Profiles.FindAsync(profileId);
        if (profile == null)
        {
            throw new InvalidOperationException($"Profile with Id {profileId} not found.");
        }

        if (profile is IndividualProfile individualProfile)
        {
            if (!string.IsNullOrWhiteSpace(request.FirstName))
                individualProfile.FirstName = request.FirstName;
            if (!string.IsNullOrWhiteSpace(request.LastName))
                individualProfile.LastName = request.LastName;
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                individualProfile.PhoneNumber = request.PhoneNumber;
        }
        else if (profile is CorporateProfile corporateProfile)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
                corporateProfile.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.EconomicId))
            {
                var existingCorporateProfile = await _profileDbContext.CorporateProfiles
                    .FirstOrDefaultAsync(p => p.EconomicId == request.EconomicId && p.Id != profileId);

                if (existingCorporateProfile != null)
                {
                    throw new InvalidOperationException($"Corporate profile with EconomicId {request.EconomicId} already exists.");
                }
                corporateProfile.EconomicId = request.EconomicId;
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

