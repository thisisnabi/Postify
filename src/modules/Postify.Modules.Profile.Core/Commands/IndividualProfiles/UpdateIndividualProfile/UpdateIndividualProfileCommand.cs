using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Commands.IndividualProfiles.UpdateIndividualProfile;

public record UpdateIndividualProfileCommand(
    long Id,
    string FirstName,
    string LastName,
    string PhoneNumber
) : IRequest;


internal sealed class UpdateIndividualProfileCommandHandler(IProfileDbContext _context)
    : IRequestHandler<UpdateIndividualProfileCommand>
{

    public async Task Handle(
        UpdateIndividualProfileCommand request,
        CancellationToken cancellationToken)
    {
        var profile = await _context.IndividualProfiles
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (profile is null)
            throw new ServiceErrorException(
                Error.NotFound(
                    description: "پروفایل مورد نظر یافت نشد.",
                    code: "profile.Individual.not_found"
                )
            );
        //throw new InvalidOperationException("Profile not found");

        profile.FirstName = request.FirstName;
        profile.LastName = request.LastName;
        profile.PhoneNumber = request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

