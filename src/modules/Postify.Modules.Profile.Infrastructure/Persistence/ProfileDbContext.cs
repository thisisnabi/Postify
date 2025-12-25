using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Entities;
using Postify.Shared.Infrastructure.Persistence;

namespace Postify.Modules.Profile.Infrastructure.Persistence;

public class ProfileDbContext : ModuleDbContext, IProfileDbContext
{
    protected override string Schema => "Profile";

    public DbSet<ProfileBase> Profiles { get; set; }
    public DbSet<CorporateProfile> CorporateProfiles { get; set; }
    public DbSet<IndividualProfile> IndividualProfiles { get; set; }

    public ProfileDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileBase>().UseTpcMappingStrategy();
        base.OnModelCreating(modelBuilder);
    }
}
