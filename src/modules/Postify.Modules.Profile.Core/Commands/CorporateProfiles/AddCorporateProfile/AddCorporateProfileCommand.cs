using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Entities;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Commands.CorporateProfiles.AddCorporateProfile;

public record AddCorporateProfileCommand(
    long OwnerId,
    string NationalId,
    string Name,
    string EconomicId
) : IRequest<long>;


internal sealed class AddCorporateProfileCommandHandler(IProfileDbContext _context) : IRequestHandler<AddCorporateProfileCommand, long>
{
    public async Task<long> Handle(AddCorporateProfileCommand request, CancellationToken cancellationToken)
    {
        var owner = await _context.IndividualProfiles
            .FirstOrDefaultAsync(x => x.Id == request.OwnerId, cancellationToken);

        if (owner is null)
            throw new ServiceErrorException(
                Error.NotFound(
                    description: "پروفایل مورد نظر یافت نشد.",
                    code: "profile.individual.not_found"
                )
            );
        //throw new InvalidOperationException("Owner profile not found");

        var corporate = new CorporateProfile
        {
            NationalId = request.NationalId,
            Name = request.Name,
            EconomicId = request.EconomicId,
            OwnerId = owner.Id
        };

        owner.CorporateProfiles.Add(corporate);
        await _context.SaveChangesAsync(cancellationToken);
        return corporate.Id;
    }
}