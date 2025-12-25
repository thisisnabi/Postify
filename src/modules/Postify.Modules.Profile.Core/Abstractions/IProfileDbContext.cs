using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Entities;

namespace Postify.Modules.Profile.Core.Abstractions;

public interface IProfileDbContext
{ 
    public DbSet<ProfileBase> Profiles { get; set; }
    public DbSet<CorporateProfile> CorporateProfiles { get; set; }
    public DbSet<IndividualProfile> IndividualProfiles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
