using MediatR;
using Postify.Modules.Profile.Core.Abstractions;
using Postify.Modules.Profile.Core.Entities;

namespace Postify.Modules.Profile.Core.Commands.IndividualProfiles.CreateIndividualProfile;

public record CreateIndividualProfileCommand(string NationalId,
    string FirstName,
    string LastName,
    string PhoneNumber
) : IRequest<long>;

internal sealed class CreateIndividualProfileCommandHandler(IProfileDbContext _context) : IRequestHandler<CreateIndividualProfileCommand, long>
{
    public async Task<long> Handle(CreateIndividualProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = new IndividualProfile
        {
            NationalId = request.NationalId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        };

        _context.IndividualProfiles.Add(profile);
        await _context.SaveChangesAsync(cancellationToken);
        return profile.Id;
    }
}