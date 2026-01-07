namespace Postify.Modules.Profile.Core.Queries._DTOs;

public class IndividualProfileDto
{
    public long Id { get; init; }
    public string NationalId { get; init; } = null!;
    public string FullName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
}
