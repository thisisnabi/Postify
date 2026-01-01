using Microsoft.EntityFrameworkCore;
using Postify.Modules.Notify.Core.Entities;

namespace Postify.Modules.Notify.Core.Abstractions;

public interface ISmsDbContext
{
    public DbSet<SmsNotificationEvent> SmsNotificationEvents { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
