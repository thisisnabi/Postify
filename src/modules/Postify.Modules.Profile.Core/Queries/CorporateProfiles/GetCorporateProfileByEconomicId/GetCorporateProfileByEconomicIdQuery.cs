using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Queries._DTOs;

namespace Postify.Modules.Profile.Core.Queries.CorporateProfile.GetCorporateProfileByEconomicId;

public record GetCorporateProfileByEconomicIdQuery(string EconomicId) : IRequest<CorporateProfileDto?>;


internal sealed class GetCorporateProfileByEconomicIdQueryHandler(IProfileDbContext _context)
            : IRequestHandler<GetCorporateProfileByEconomicIdQuery, CorporateProfileDto?>
{
    public async Task<CorporateProfileDto?> Handle(
        GetCorporateProfileByEconomicIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.CorporateProfiles
            .Where(x => x.EconomicId == request.EconomicId)
            .Select(x => new CorporateProfileDto
            {
                Id = x.Id,
                Name = x.Name,
                EconomicId = x.EconomicId
            }).FirstOrDefaultAsync(cancellationToken);
    }
}