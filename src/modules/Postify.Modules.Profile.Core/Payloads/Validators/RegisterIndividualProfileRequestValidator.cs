using FluentValidation;
using Postify.Modules.Profile.Core.Common.Validators;

namespace Postify.Modules.Profile.Core.Payloads.Validators;

internal class RegisterIndividualProfileRequestValidator : BaseValidator<RegisterIndividualProfileRequest>
{
    public RegisterIndividualProfileRequestValidator()
    {
        RuleFor(request => request.NationalId)
            .NotEmpty()
            .WithMessage("NationalId is required.")
            .Length(11)
            .WithMessage("NationalId must be exactly 11 characters.");

        RuleFor(request => request.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.")
            .MaximumLength(50)
            .WithMessage("FirstName cannot exceed 50 characters.");

        RuleFor(request => request.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.")
            .MaximumLength(50)
            .WithMessage("LastName cannot exceed 50 characters.");

        RuleFor(request => request.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required.")
            .MaximumLength(15)
            .WithMessage("PhoneNumber cannot exceed 15 characters.");
    }
}

