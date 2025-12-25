namespace Postify.Modules.Profile.Core.Payloads;

public class UpdateProfileRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Name { get; set; }
    public string? EconomicId { get; set; }
}

