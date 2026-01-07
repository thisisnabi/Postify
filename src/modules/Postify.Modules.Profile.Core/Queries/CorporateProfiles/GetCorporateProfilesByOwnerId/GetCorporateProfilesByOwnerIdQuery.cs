using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Queries._DTOs;

namespace Postify.Modules.Profile.Core.Queries.CorporateProfile.GetCorporateProfilesByOwner;

public record GetCorporateProfilesByOwnerIdQuery(long OwnerId) : IRequest<IReadOnlyList<CorporateProfileDto>>;

internal sealed class GetCorporateProfilesByOwnerIdQueryHandler(IProfileDbContext _context) : IRequestHandler<GetCorporateProfilesByOwnerIdQuery, IReadOnlyList<CorporateProfileDto>>
{
    public async Task<IReadOnlyList<CorporateProfileDto>> Handle(
        GetCorporateProfilesByOwnerIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.CorporateProfiles
            .Where(x => x.OwnerId == request.OwnerId)
            .Select(x => new CorporateProfileDto
            {
                Id = x.Id,
                Name = x.Name,
                EconomicId = x.EconomicId
            }).ToListAsync(cancellationToken);
    }
}