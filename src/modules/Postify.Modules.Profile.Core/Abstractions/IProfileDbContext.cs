using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Entities;

namespace Postify.Modules.Profile.Core.Abstractions;

public interface IProfileDbContext
{ 
    DbSet<ProfileBase> Profiles { get; set; }
    DbSet<CorporateProfile> CorporateProfiles { get; set; }
    DbSet<IndividualProfile> IndividualProfiles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
