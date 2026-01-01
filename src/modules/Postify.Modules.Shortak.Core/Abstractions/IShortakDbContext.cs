using Microsoft.EntityFrameworkCore;
using Postify.Modules.Shortak.Core.Entities;

namespace Postify.Modules.Shortak.Core.Abstractions;
public interface IShortakDbContext
{
    public DbSet<ShortUrl> ShortUrls { get; set; }
}
