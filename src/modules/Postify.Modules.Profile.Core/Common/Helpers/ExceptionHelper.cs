using FluentValidation.Results;

namespace Postify.Modules.Profile.Core.Common.Helpers;

public static class ExceptionHelper
{
    public static ArgumentException BadRequest(ValidationResult validationResult)
    {
        var firstError = validationResult.Errors.FirstOrDefault();
        if (firstError != null)
        {
            return new ArgumentException(firstError.ErrorMessage);
        }
        return new ArgumentException("Validation failed.");
    }

    public static ArgumentException BadRequest(string message)
    {
        return new ArgumentException(message);
    }
}

