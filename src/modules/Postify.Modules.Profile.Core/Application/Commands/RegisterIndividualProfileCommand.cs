namespace Postify.Modules.Profile.Core.Application.Commands;

public record RegisterIndividualProfileCommand(
    string NationalId,
    string FirstName,
    string LastName,
    string PhoneNumber
);

