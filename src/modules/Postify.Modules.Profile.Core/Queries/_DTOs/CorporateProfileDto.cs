namespace Postify.Modules.Profile.Core.Queries._DTOs;

public class CorporateProfileDto
{
    public long Id { get; init; }
    public string Name { get; init; } = null!;
    public string EconomicId { get; init; } = null!;
}
