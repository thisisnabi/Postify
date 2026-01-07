namespace Postify.Modules.Profile.Core.Application.Commands;

public record RegisterCorporateProfileCommand(
    string NationalId,
    string Name,
    string EconomicId
);

