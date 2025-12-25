using FluentValidation;
using Postify.Modules.Profile.Core.Common.Validators;

namespace Postify.Modules.Profile.Core.Payloads.Validators;

internal class RegisterCorporateProfileRequestValidator : BaseValidator<RegisterCorporateProfileRequest>
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

