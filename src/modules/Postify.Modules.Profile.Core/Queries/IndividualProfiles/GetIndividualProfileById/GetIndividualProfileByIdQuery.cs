using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Queries._DTOs;

namespace Postify.Modules.Profile.Core.Queries.IndividualProfiles.GetIndividualProfile;

public record GetIndividualProfileByIdQuery(long Id)
    : IRequest<IndividualProfileDto?>;

internal sealed class GetIndividualProfileByIdQueryHandler(IProfileDbContext _context) 
    : IRequestHandler<GetIndividualProfileByIdQuery, IndividualProfileDto?>
{
    public async Task<IndividualProfileDto?> Handle(
        GetIndividualProfileByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.IndividualProfiles
            .Where(x => x.Id == request.Id)
            .Select(x => new IndividualProfileDto
            {
                Id = x.Id,
                NationalId = x.NationalId,
                FullName = x.FirstName + " " + x.LastName,
                PhoneNumber = x.PhoneNumber
            }).FirstOrDefaultAsync(cancellationToken);
    }
}