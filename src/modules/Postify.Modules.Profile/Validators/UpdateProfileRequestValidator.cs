using FluentValidation;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Modules.Profile.Validators;

namespace Postify.Modules.Profile.Validators;

public class UpdateProfileRequestValidator : BaseValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .MaximumLength(50)
            .WithMessage("FirstName cannot exceed 50 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.FirstName));

        RuleFor(request => request.LastName)
            .MaximumLength(50)
            .WithMessage("LastName cannot exceed 50 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.LastName));

        RuleFor(request => request.PhoneNumber)
            .MaximumLength(15)
            .WithMessage("PhoneNumber cannot exceed 15 characters.")
            .When(request => !string.IsNullOrWhiteSpace(request.PhoneNumber));
    }
}

