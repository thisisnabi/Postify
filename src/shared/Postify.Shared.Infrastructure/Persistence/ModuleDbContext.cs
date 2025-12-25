using Microsoft.EntityFrameworkCore;

namespace Postify.Shared.Infrastructure.Persistence;

public abstract class ModuleDbContext : DbContext
{
    protected abstract string Schema { get; }
     
    public ModuleDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrWhiteSpace(Schema))
            modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
    }
}
