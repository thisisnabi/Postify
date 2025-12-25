using FluentAssertions;
using Postify.Modules.Profile.Core.Application;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Infrastructure.Persistence;
using Postify.Profile.Tests.Application.Profiles.TestData;
using Postify.Profile.Tests.Common.TestHelpers;

namespace Postify.Profile.Tests.Application.Profiles;

public class RegisterIndividualProfileTests : IDisposable
{
    private readonly ProfileDbContext _dbContext;
    private readonly ProfileService _profileService;

    public RegisterIndividualProfileTests()
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
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId(string.Empty)
            .WithFirstName(5)
            .WithLastName(5)
            .WithPhoneNumber(11)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("NationalId is required.");
    }

    [Fact]
    public async Task national_id_length_is_invalid_returns_error_message()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("1234567890")
            .WithFirstName(5)
            .WithLastName(5)
            .WithPhoneNumber(11)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("NationalId must be exactly 11 characters.");
    }

    [Fact]
    public async Task first_name_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithFirstName(0)
            .WithLastName(5)
            .WithPhoneNumber(11)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("FirstName is required.");
    }

    [Fact]
    public async Task lengthy_first_name_returns_error_message()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithFirstName(51)
            .WithLastName(5)
            .WithPhoneNumber(11)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("FirstName cannot exceed 50 characters.");
    }

    [Fact]
    public async Task last_name_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithFirstName(5)
            .WithLastName(0)
            .WithPhoneNumber(11)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("LastName is required.");
    }

    [Fact]
    public async Task lengthy_last_name_returns_error_message()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithFirstName(5)
            .WithLastName(51)
            .WithPhoneNumber(11)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("LastName cannot exceed 50 characters.");
    }

    [Fact]
    public async Task phone_number_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithFirstName(5)
            .WithLastName(5)
            .WithPhoneNumber(0)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("PhoneNumber is required.");
    }

    [Fact]
    public async Task lengthy_phone_number_returns_error_message()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithFirstName(5)
            .WithLastName(5)
            .WithPhoneNumber(16)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<ArgumentException>();
        exception.WithMessage("PhoneNumber cannot exceed 15 characters.");
    }

    [Fact]
    public async Task national_id_already_exists_returns_error_message()
    {
        // Arrange
        var nationalId = "12345678901";
        var existingProfile = new IndividualProfile
        {
            NationalId = nationalId,
            FirstName = "Existing",
            LastName = "User",
            PhoneNumber = "09123456789"
        };
        _dbContext.IndividualProfiles.Add(existingProfile);
        await _dbContext.SaveChangesAsync();

        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId(nationalId)
            .WithPhoneNumber(11)
            .Create();

        // Act
        var func = async () => await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var exception = await func.Should().ThrowAsync<InvalidOperationException>();
        exception.WithMessage($"Profile with NationalId {nationalId} already exists.");
    }

    [Fact]
    public async Task add_new_individual_profile_without_initial()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithPhoneNumber(11)
            .Create();

        // Act
        var result = await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.NationalId.Should().Be(request.NationalId);
        result.ProfileType.Should().Be("Individual");
        result.Id.Should().BeGreaterThan(0);

        var savedProfile = await _dbContext.IndividualProfiles.FindAsync(result.Id);
        savedProfile.Should().NotBeNull();
        savedProfile!.NationalId.Should().Be(request.NationalId);
        savedProfile.FirstName.Should().Be(request.FirstName);
        savedProfile.LastName.Should().Be(request.LastName);
        savedProfile.PhoneNumber.Should().Be(request.PhoneNumber);
    }

    [Fact]
    public async Task add_new_individual_profile_saves_to_database()
    {
        // Arrange
        var request = new RegisterIndividualProfileRequestTestData()
            .WithNationalId("12345678901")
            .WithPhoneNumber(11)
            .Create();

        // Act
        var result = await _profileService.RegisterIndividualProfileAsync(request);

        // Assert
        var savedProfile = await _dbContext.IndividualProfiles.FindAsync(result.Id);

        savedProfile.Should().NotBeNull();
        savedProfile!.NationalId.Should().Be(request.NationalId);
        savedProfile.FirstName.Should().Be(request.FirstName);
        savedProfile.LastName.Should().Be(request.LastName);
        savedProfile.PhoneNumber.Should().Be(request.PhoneNumber);
    }
}

