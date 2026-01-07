using FluentValidation;
using Postify.Modules.Profile.Helpers;

namespace Postify.Modules.Profile.Helpers;

public static class ValidationHelper
{
    public static void ValidateAndThrow<T>(IValidator<T> validator, T instance)
    {
        var result = validator.Validate(instance);
        if (!result.IsValid)
        {
            throw ExceptionHelper.ValidationError(result);
        }
    }
}

