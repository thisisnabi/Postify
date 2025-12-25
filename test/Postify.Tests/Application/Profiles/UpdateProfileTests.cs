using FluentAssertions;
using Postify.Modules.Profile.Core.Application;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Infrastructure.Persistence;
using Postify.Profile.Tests.Application.Profiles.TestData;
using Postify.Profile.Tests.Common.TestHelpers;

namespace Postify.Profile.Tests.Application.Profiles;

public class UpdateProfileTests : IDisposable
{
    private readonly ProfileDbContext _dbContext;
    private readonly ProfileService _profileService;

    public UpdateProfileTests()
    {
        _dbContext = InMemoryProfileDbContextFactory.Create();
        _profileService = new ProfileService(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Fact]
    public async Task profile_not_found_returns_error_message()
    {
        // Arrange
        var profileId = 999L;
        var request = new UpdateProfileRequestTestData()
            .Create();

        // Act
        var func = async () => await _profileService.UpdateProfileAsync(profileId, request);

        // Assert
        var exception = await func.Should().ThrowAsync<InvalidOperationException>();
        exception.WithMessage($"Profile with Id {profileId} not found.");
    }

    [Fact]
    public async Task lengthy_first_name_returns_error_message()
    {
        // Arrange
        var existingProfile = new IndividualProfile
        {
            NationalId = "12345678901",
            FirstName = "Existing",
            LastName = "User",
            PhoneNumber = "09123456789"
        };
        _dbContext.IndividualProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateProfileRequestTestData()
            .WithFirstName(51)
            .Create();

        // Act
        var func = async () => await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("FirstName cannot exceed 50 characters.");
    }

    [Fact]
    public async Task lengthy_last_name_returns_error_message()
    {
        // Arrange
        var existingProfile = new IndividualProfile
        {
            NationalId = "12345678901",
            FirstName = "Existing",
            LastName = "User",
            PhoneNumber = "09123456789"
        };
        _dbContext.IndividualProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateProfileRequestTestData()
            .WithLastName(51)
            .Create();

        // Act
        var func = async () => await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("LastName cannot exceed 50 characters.");
    }

    [Fact]
    public async Task lengthy_phone_number_returns_error_message()
    {
        // Arrange
        var existingProfile = new IndividualProfile
        {
            NationalId = "12345678901",
            FirstName = "Existing",
            LastName = "User",
            PhoneNumber = "09123456789"
        };
        _dbContext.IndividualProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateProfileRequestTestData()
            .WithPhoneNumber(16)
            .Create();

        // Act
        var func = async () => await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("PhoneNumber cannot exceed 15 characters.");
    }

    [Fact]
    public async Task updating_corporate_profile_with_duplicate_economic_id_returns_error_message()
    {
        // Arrange
        var economicId = "1234567890";
        var existingProfile = new CorporateProfile
        {
            NationalId = "12345678901",
            Name = "Test Corp",
            EconomicId = "9876543210"
        };
        _dbContext.CorporateProfiles.Add(existingProfile);

        var duplicateProfile = new CorporateProfile
        {
            NationalId = "98765432109",
            Name = "Other Corp",
            EconomicId = economicId
        };
        _dbContext.CorporateProfiles.Add(duplicateProfile);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateProfileRequestTestData()
            .WithEconomicId(economicId)
            .Create();

        // Act
        var func = async () => await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        var exception = await func.Should().ThrowAsync<InvalidOperationException>();
        exception.WithMessage($"Corporate profile with EconomicId {economicId} already exists.");
    }

    [Fact]
    public async Task update_individual_profile()
    {
        // Arrange
        var existingProfile = new IndividualProfile
        {
            NationalId = "12345678901",
            FirstName = "Existing",
            LastName = "User",
            PhoneNumber = "09123456789"
        };
        _dbContext.IndividualProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateProfileRequestTestData()
            .Create();

        // Act
        var result = await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(existingProfile.Id);
        result.NationalId.Should().Be(existingProfile.NationalId);
        result.ProfileType.Should().Be("Individual");

        var updatedProfile = await _dbContext.IndividualProfiles.FindAsync(existingProfile.Id);
        updatedProfile.Should().NotBeNull();
        updatedProfile!.FirstName.Should().Be(existingProfile.FirstName);
        updatedProfile.LastName.Should().Be(existingProfile.LastName);
        updatedProfile.PhoneNumber.Should().Be(existingProfile.PhoneNumber);
    }

    [Fact]
    public async Task update_corporate_profile()
    {
        // Arrange
        var existingProfile = new CorporateProfile
        {
            NationalId = "12345678901",
            Name = "Original Corp",
            EconomicId = "1234567890"
        };
        _dbContext.CorporateProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateProfileRequestTestData()
            .WithEconomicId("9876543210")
            .Create();

        // Act
        var result = await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(existingProfile.Id);
        result.NationalId.Should().Be(existingProfile.NationalId);
        result.ProfileType.Should().Be("Corporate");

        var updatedProfile = await _dbContext.CorporateProfiles.FindAsync(existingProfile.Id);
        updatedProfile.Should().NotBeNull();
        updatedProfile!.Name.Should().Be(existingProfile.Name);
        updatedProfile.EconomicId.Should().Be(request.EconomicId);
    }

    [Fact]
    public async Task update_profile_with_partial_fields()
    {
        // Arrange
        var existingProfile = new IndividualProfile
        {
            NationalId = "12345678901",
            FirstName = "Existing",
            LastName = "User",
            PhoneNumber = "09123456789"
        };
        _dbContext.IndividualProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var originalLastName = existingProfile.LastName;
        var originalPhoneNumber = existingProfile.PhoneNumber;

        var request = new UpdateProfileRequestTestData()
            .Create();

        // Act
        var result = await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        result.Should().NotBeNull();
        var updatedProfile = await _dbContext.IndividualProfiles.FindAsync(existingProfile.Id);
        updatedProfile.Should().NotBeNull();
        updatedProfile!.FirstName.Should().Be(existingProfile.FirstName);
        updatedProfile.LastName.Should().Be(originalLastName);
        updatedProfile.PhoneNumber.Should().Be(originalPhoneNumber);
    }

    [Fact]
    public async Task update_corporate_profile_with_same_economic_id()
    {
        // Arrange
        var existingProfile = new CorporateProfile
        {
            NationalId = "12345678901",
            Name = "Test Corp",
            EconomicId = "1234567890"
        };
        _dbContext.CorporateProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateProfileRequestTestData()
            .WithEconomicId("1234567890")
            .Create();

        // Act
        var result = await _profileService.UpdateProfileAsync(existingProfile.Id, request);

        // Assert
        result.Should().NotBeNull();
        var updatedProfile = await _dbContext.CorporateProfiles.FindAsync(existingProfile.Id);
        updatedProfile.Should().NotBeNull();
        updatedProfile!.Name.Should().Be(existingProfile.Name);
        updatedProfile.EconomicId.Should().Be(request.EconomicId);
    }
}

