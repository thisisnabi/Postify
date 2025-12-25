using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Infrastructure.Persistence;
using Postify.Shared.Infrastructure.Persistence;

namespace Postify.Profile.Tests.Common.TestHelpers;

public static class InMemoryProfileDbContextFactory
{
    public static ProfileDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ModuleDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ProfileDbContext(options);
    }
}

