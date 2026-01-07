using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Commands.IndividualProfiles.ChangeIndividualNationalId;

public record ChangeIndividualNationalIdCommand(
    long Id,
    string NationalId
) : IRequest;

internal sealed class ChangeIndividualNationalIdCommandHandler(IProfileDbContext _context)
    : IRequestHandler<ChangeIndividualNationalIdCommand>
{

    public async Task Handle(
        ChangeIndividualNationalIdCommand request,
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

        profile.NationalId = request.NationalId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}