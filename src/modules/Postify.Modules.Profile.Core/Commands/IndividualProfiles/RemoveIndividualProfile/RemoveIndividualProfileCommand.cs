using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Commands.IndividualProfiles.RemoveIndividualProfile;

public record RemoveIndividualProfileCommand(long Id) : IRequest;

internal sealed class RemoveIndividualProfileCommandHandler(IProfileDbContext _context)
    : IRequestHandler<RemoveIndividualProfileCommand>
{
    public async Task Handle(
        RemoveIndividualProfileCommand request,
        CancellationToken cancellationToken)
    {
        var profile = await _context.IndividualProfiles
            .Include(x => x.CorporateProfiles)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (profile is null)
            throw new ServiceErrorException(
                Error.NotFound(
                    description: "پروفایل مورد نظر یافت نشد.",
                    code: "profile.Individual.not_found"
                )
            );
        //throw new InvalidOperationException("Profile not found");

        _context.IndividualProfiles.Remove(profile);

        await _context.SaveChangesAsync(cancellationToken);
    }
}