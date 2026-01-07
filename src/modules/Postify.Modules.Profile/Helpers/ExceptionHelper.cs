using FluentValidation.Results;
using Postify.Shared.Kernel.Errors;

namespace Postify.Modules.Profile.Helpers;

public static class ExceptionHelper
{
    public static ServiceErrorException ValidationError(ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        var firstError = validationResult.Errors.FirstOrDefault();
        
        var metadata = new Dictionary<string, string>();
        for (int i = 0; i < errors.Count; i++)
        {
            metadata[$"error_{i}"] = errors[i];
        }

        return new ServiceErrorException(
            Error.Validation(
                title: "خطای اعتبارسنجی",
                description: firstError?.ErrorMessage ?? "خطایی در اعتبارسنجی رخ داده است.",
                code: "validation.failed",
                metadata: metadata
            )
        );
    }
}

