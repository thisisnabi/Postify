using Microsoft.EntityFrameworkCore;
using Postify.Modules.Notify.Core.Abstractions;
using Postify.Modules.Notify.Core.Entities;
using Postify.Shared.Infrastructure.Persistence;

namespace Postify.Modules.Notify.Infrastructure.Persistence;

public class SmsDbContext : ModuleDbContext, ISmsDbContext
{
    protected override string Schema => "sms";

    public DbSet<SmsNotificationEvent> SmsNotificationEvents { get; set; }

    public SmsDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
    {

    }
}
