using Microsoft.EntityFrameworkCore;
using Postify.Modules.Media.Core.Abstractions;
using Postify.Modules.Media.Core.Entities;
using Postify.Shared.Infrastructure.Persistence;

namespace Postify.Modules.Media.Infrastructure.Persistence;
public class MediaDbContext : ModuleDbContext, IMediaDbContext
{
    public DbSet<ObjectFile> ObjectFiles { get; set; }

    protected override string Schema => "media";

    public MediaDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
    {

    }
}
