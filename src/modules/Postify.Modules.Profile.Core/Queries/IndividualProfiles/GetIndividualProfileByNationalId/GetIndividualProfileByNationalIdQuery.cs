using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Queries._DTOs;

namespace Postify.Modules.Profile.Core.Queries.IndividualProfiles.GetIndividualProfileByNationalId;

public record GetIndividualProfileByNationalIdQuery(
    string NationalId
) : IRequest<IndividualProfileDto?>;


internal sealed class GetIndividualProfileByNationalIdQueryHandler(IProfileDbContext _context)
    : IRequestHandler<GetIndividualProfileByNationalIdQuery, IndividualProfileDto?>
{
    public async Task<IndividualProfileDto?> Handle(
        GetIndividualProfileByNationalIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.IndividualProfiles
            .Where(x => x.NationalId == request.NationalId)
            .Select(x => new IndividualProfileDto
            {
                Id = x.Id,
                NationalId = x.NationalId,
                FullName = x.FirstName + " " + x.LastName,
                PhoneNumber = x.PhoneNumber
            }).FirstOrDefaultAsync(cancellationToken);
    }
}