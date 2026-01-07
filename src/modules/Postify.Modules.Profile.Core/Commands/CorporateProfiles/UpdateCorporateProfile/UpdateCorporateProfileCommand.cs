using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Commands.CorporateProfiles.UpdateCorporateProfile;

public record UpdateCorporateProfileCommand(
    long Id,
    string Name,
    string EconomicId
) : IRequest;

internal sealed class UpdateCorporateProfileCommandHandler(IProfileDbContext _context)
    : IRequestHandler<UpdateCorporateProfileCommand>
{
    public async Task Handle(
        UpdateCorporateProfileCommand request,
        CancellationToken cancellationToken)
    {
        var corporate = await _context.CorporateProfiles
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (corporate is null)
            throw new ServiceErrorException(
                Error.NotFound(
                    description: "پروفایل مورد نظر یافت نشد.",
                    code: "profile.Corporate.not_found"
                )
            );
        //throw new InvalidOperationException("Corporate profile not found");

        corporate.Name = request.Name;
        corporate.EconomicId = request.EconomicId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}