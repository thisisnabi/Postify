using FluentValidation;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Modules.Profile.Validators;

namespace Postify.Modules.Profile.Validators;

public class RegisterCorporateProfileRequestValidator : BaseValidator<RegisterCorporateProfileRequest>
{
    public RegisterCorporateProfileRequestValidator()
    {
        RuleFor(request => request.NationalId)
            .NotEmpty()
            .WithMessage("NationalId is required.")
            .Length(11)
            .WithMessage("NationalId must be exactly 11 characters.");

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(request => request.EconomicId)
            .NotEmpty()
            .WithMessage("EconomicId is required.");
    }
}

