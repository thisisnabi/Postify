namespace Postify.Modules.Profile.Core.Payloads;

public class RegisterIndividualProfileRequest
{
    public required string NationalId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
}

