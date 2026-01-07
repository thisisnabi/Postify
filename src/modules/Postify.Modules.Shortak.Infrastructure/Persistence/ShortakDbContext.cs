using Microsoft.EntityFrameworkCore;
using Postify.Modules.Shortak.Core.Abstractions;
using Postify.Modules.Shortak.Core.Entities;
using Postify.Shared.Infrastructure.Persistence;

namespace Postify.Modules.Shortak.Infrastructure.Persistence;
public class ShortakDbContext : ModuleDbContext, IShortakDbContext
{
    public DbSet<ShortUrl> ShortUrls { get; set; }

    protected override string Schema => "shortak";

    public ShortakDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
    {

    }
}
