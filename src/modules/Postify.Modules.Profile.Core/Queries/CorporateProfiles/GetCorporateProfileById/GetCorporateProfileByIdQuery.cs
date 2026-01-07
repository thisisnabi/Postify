using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Queries._DTOs;

namespace Postify.Modules.Profile.Core.Queries.CorporateProfile.GetCorporateProfileById;

public record GetCorporateProfileByIdQuery(long Id) : IRequest<CorporateProfileDto?>;


internal sealed class GetCorporateProfileByIdQueryHandler(IProfileDbContext _context)
    : IRequestHandler<GetCorporateProfileByIdQuery, CorporateProfileDto?>
{
    public async Task<CorporateProfileDto?> Handle(
        GetCorporateProfileByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.CorporateProfiles
            .Where(x => x.Id == request.Id)
            .Select(x => new CorporateProfileDto
            {
                Id = x.Id,
                Name = x.Name,
                EconomicId = x.EconomicId
            }).FirstOrDefaultAsync(cancellationToken);
    }
}