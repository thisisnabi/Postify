namespace Postify.Modules.Profile.Core.Payloads;

public class RegisterCorporateProfileRequest
{
    public required string NationalId { get; set; }
    public required string Name { get; set; }
    public required string EconomicId { get; set; }
}

