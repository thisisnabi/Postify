using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;

namespace Postify.Modules.Profile.Core.Queries.CorporateProfile.HasCorporateProfile;

public record HasCorporateProfileQuery(long IndividualId) : IRequest<bool>;

internal sealed class HasCorporateProfileQueryHandler(IProfileDbContext _context)
    : IRequestHandler<HasCorporateProfileQuery, bool>
{
    public async Task<bool> Handle(
        HasCorporateProfileQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.CorporateProfiles
            .AnyAsync(x => x.OwnerId == request.IndividualId, cancellationToken);
    }
}