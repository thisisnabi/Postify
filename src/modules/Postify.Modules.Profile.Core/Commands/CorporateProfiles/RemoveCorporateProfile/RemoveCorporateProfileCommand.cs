using MediatR;
using Microsoft.EntityFrameworkCore;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Core.Commands.CorporateProfiles.RemoveCorporateProfile;

public record RemoveCorporateProfileCommand(long Id) : IRequest;

internal sealed class RemoveCorporateProfileCommandHandler(IProfileDbContext _context)
    : IRequestHandler<RemoveCorporateProfileCommand>
{
    public async Task Handle(
        RemoveCorporateProfileCommand request,
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

        _context.CorporateProfiles.Remove(corporate);

        await _context.SaveChangesAsync(cancellationToken);
    }
}