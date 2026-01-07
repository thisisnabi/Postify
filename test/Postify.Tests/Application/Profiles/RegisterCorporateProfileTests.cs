using FluentAssertions;
using Postify.Modules.Profile.Core.Application.Commands;
using Postify.Modules.Profile.Core.Entities;
using Postify.Modules.Profile.Infrastructure.Persistence;
using Postify.Modules.Profile.Helpers;
using Postify.Modules.Profile.Validators;
using Postify.Profile.Tests.Application.Profiles.TestData;
using Postify.Profile.Tests.Common.TestHelpers;
using Postify.Shared.Kernel.Errors;

namespace Postify.Profile.Tests.Application.Profiles;

public class RegisterCorporateProfileTests : IDisposable
{
    private readonly ProfileDbContext _dbContext;
    private readonly RegisterCorporateProfileCommandHandler _handler;
    private readonly RegisterCorporateProfileRequestValidator _validator;

    public RegisterCorporateProfileTests()
    {
        _dbContext = InMemoryProfileDbContextFactory.Create();
        _handler = new RegisterCorporateProfileCommandHandler(_dbContext);
        _validator = new RegisterCorporateProfileRequestValidator();
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
            .Create();

        // Act
        var func = () => ValidationHelper.ValidateAndThrow(_validator, request);

        // Assert
        var exception = func.Should().Throw<ServiceErrorException>();
        exception.Which.Error.Type.Should().Be(ErrorType.Validation);
        exception.Which.Error.Description.Should().Contain("NationalId is required.");
    }

    [Fact]
    public async Task national_id_length_is_invalid_returns_error_message()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("123456789012")
            .Create();

        // Act
        var func = () => ValidationHelper.ValidateAndThrow(_validator, request);

        // Assert
        var exception = func.Should().Throw<ServiceErrorException>();
        exception.Which.Error.Type.Should().Be(ErrorType.Validation);
        exception.Which.Error.Description.Should().Contain("NationalId must be exactly 11 characters");
    }

    [Fact]
    public async Task name_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901") // ensure NationalId is valid so Name validation is first
            .WithEconomicId("123") // ensure EconomicId is valid
            .WithName(0)
            .Create();

        // Act
        var func = () => ValidationHelper.ValidateAndThrow(_validator, request);

        // Assert
        var exception = func.Should().Throw<ServiceErrorException>();
        exception.Which.Error.Type.Should().Be(ErrorType.Validation);
        exception.Which.Error.Description.Should().Contain("Name is required.");
    }

    [Fact]
    public async Task economic_id_is_empty_returns_error_message()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901") // ensure NationalId is valid so EconomicId validation is first
            .WithName(5) // ensure Name is valid
            .WithEconomicId(0)
            .Create();

        // Act
        var func = () => ValidationHelper.ValidateAndThrow(_validator, request);

        // Assert
        var exception = func.Should().Throw<ServiceErrorException>();
        exception.Which.Error.Type.Should().Be(ErrorType.Validation);
        exception.Which.Error.Description.Should().Contain("EconomicId is required.");
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
            .Create();

        // Act
        var command = new RegisterCorporateProfileCommand(
            request.NationalId,
            request.Name,
            request.EconomicId
        );
        var func = async () => await _handler.HandleAsync(command);

        // Assert
        var exception = await func.Should().ThrowAsync<ServiceErrorException>();
        exception.Which.Error.Type.Should().Be(ErrorType.Failure);
        exception.Which.Error.Description.Should().Contain(nationalId);
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
            .WithEconomicId(economicId)
            .Create();

        // Act
        var command = new RegisterCorporateProfileCommand(
            request.NationalId,
            request.Name,
            request.EconomicId
        );
        var func = async () => await _handler.HandleAsync(command);

        // Assert
        var exception = await func.Should().ThrowAsync<ServiceErrorException>();
        exception.Which.Error.Type.Should().Be(ErrorType.Failure);
        exception.Which.Error.Description.Should().Contain(economicId);
    }

    [Fact]
    public async Task add_new_corporate_profile_without_initial()
    {
        // Arrange
        var request = new RegisterCorporateProfileRequestTestData()
            .WithNationalId("12345678901")
            .Create();

        // Act
        var command = new RegisterCorporateProfileCommand(
            request.NationalId,
            request.Name,
            request.EconomicId
        );
        var result = await _handler.HandleAsync(command);

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
            .Create();

        // Act
        var command = new RegisterCorporateProfileCommand(
            request.NationalId,
            request.Name,
            request.EconomicId
        );
        var result = await _handler.HandleAsync(command);

        // Assert
        var savedProfile = await _dbContext.CorporateProfiles.FindAsync(result.Id);

        savedProfile.Should().NotBeNull();
        savedProfile!.NationalId.Should().Be(request.NationalId);
        savedProfile.Name.Should().Be(request.Name);
        savedProfile.EconomicId.Should().Be(request.EconomicId);
    }
}

