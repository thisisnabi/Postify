namespace Postify.Modules.Profile.Core.Application.Commands;

public record UpdateProfileCommand(
    long ProfileId,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Name,
    string? EconomicId
);

