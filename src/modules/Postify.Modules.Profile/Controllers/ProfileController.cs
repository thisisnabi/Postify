using Microsoft.AspNetCore.Mvc;
using Postify.Modules.Profile.Core.Application.Commands;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Modules.Profile.Helpers;
using Postify.Modules.Profile.Validators;

namespace Postify.Modules.Profile.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly UpdateProfileCommandHandler _updateHandler;
    private readonly UpdateProfileRequestValidator _updateValidator;

    public ProfileController(
        UpdateProfileCommandHandler updateHandler,
        UpdateProfileRequestValidator updateValidator)
    {
        _updateHandler = updateHandler;
        _updateValidator = updateValidator;
    }

    [HttpPut("{profileId}")]
    public async Task<ActionResult<ProfileResponse>> Update(
        [FromRoute] long profileId,
        [FromBody] UpdateProfileRequest request)
    {
        ValidationHelper.ValidateAndThrow(_updateValidator, request);

        var command = new UpdateProfileCommand(
            profileId,
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Name,
            request.EconomicId
        );

        var result = await _updateHandler.HandleAsync(command);
        return Ok(result);
    }
}
