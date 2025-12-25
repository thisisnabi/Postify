using FluentAssertions;
using Postify.Modules.Profile.Core.Application;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Infrastructure.Persistence;
using Postify.Profile.Tests.Application.Profiles.TestData;
using Postify.Profile.Tests.Common.TestHelpers;

namespace Postify.Profile.Tests.Application.Profiles;

public class RegisterCorporateProfileTests : IDisposable
{
    private readonly ProfileDbContext _dbContext;
    private readonly ProfileService _profileService;

    public RegisterCorporateProfileTests()
    {
        _dbContext = InMemoryProfileDbContextFactory.Create();
        _profileService = new ProfileService(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Fact]
    public async Task national_id_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId(string.Empty)
            .WithName(10)
            .WithEconomicId(10)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("NationalId is required.");
    }

    [Fact]
    public async Task national_id_length_is_invalid_returns_error_message()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("123456789012")
            .WithName(10)
            .WithEconomicId(10)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("NationalId must be exactly 11 characters.");
    }

    [Fact]
    public async Task name_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithName(0)
            .WithEconomicId("1234567890")
            .Create();

        // Act
        var func = async () => await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("Name is required.");
    }

    [Fact]
    public async Task economic_id_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithEconomicId(0)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("EconomicId is required.");
    }

    [Fact]
    public async Task national_id_already_exists_returns_error_message()
    {
        // Arrange
        var nationalId = "12345678901";
        var existingProfile = new CorporateProfile
        {
            NationalId = nationalId,
            Name = "Existing Corp",
            EconomicId = "1234567890"
        };
        _dbContext.CorporateProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId(nationalId)
            .WithEconomicId("9876543210")
            .Create();

        // Act
        var func = async () => await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<InvalidOperationException>();
        exception.WithMessage($"Profile with NationalId {nationalId} already exists.");
    }

    [Fact]
    public async Task economic_id_already_exists_returns_error_message()
    {
        // Arrange
        var economicId = "1234567890";
        var existingProfile = new CorporateProfile
        {
            NationalId = "98765432109",
            Name = "Existing Corp",
            EconomicId = economicId
        };
        _dbContext.CorporateProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithEconomicId(economicId)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<InvalidOperationException>();
        exception.WithMessage($"Corporate profile with EconomicId {economicId} already exists.");
    }

    [Fact]
    public async Task add_new_corporate_profile_without_initial()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithEconomicId("1234567890")
            .Create();

        // Act
        var result = await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.NationalId.Should().Be(request.NationalId);
        result.ProfileType.Should().Be("Corporate");
        result.Id.Should().BeGreaterThan(0);

        var savedProfile = await _dbContext.CorporateProfiles.FindAsync(result.Id);
        savedProfile.Should().NotBeNull();
        savedProfile!.NationalId.Should().Be(request.NationalId);
        savedProfile.Name.Should().Be(request.Name);
        savedProfile.EconomicId.Should().Be(request.EconomicId);
    }

    [Fact]
    public async Task add_new_corporate_profile_saves_to_database()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithEconomicId("1234567890")
            .Create();

        // Act
        var result = await _profileService.RegisterCorporateProfileAsync(request);

        // Assert
        var savedProfile = await _dbContext.CorporateProfiles.FindAsync(result.Id);

        savedProfile.Should().NotBeNull();
        savedProfile!.NationalId.Should().Be(request.NationalId);
        savedProfile.Name.Should().Be(request.Name);
        savedProfile.EconomicId.Should().Be(request.EconomicId);
    }
}

