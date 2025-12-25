namespace Postify.Modules.Profile.Core.Entities;

public class CorporateProfile : ProfileBase
{
    public required string Name { get; set; }

    public required string EconomicId { get; set; }
}