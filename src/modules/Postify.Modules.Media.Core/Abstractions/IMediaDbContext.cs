using Microsoft.EntityFrameworkCore;
using Postify.Modules.Media.Core.Entities;

namespace Postify.Modules.Media.Core.Abstractions;

public interface IMediaDbContext
{
    DbSet<ObjectFile> ObjectFiles { get; set; }
}
