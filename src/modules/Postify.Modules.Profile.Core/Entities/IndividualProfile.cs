namespace Postify.Modules.Profile.Core.Entities;

public class IndividualProfile : ProfileBase
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
}
