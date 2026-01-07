using Microsoft.AspNetCore.Mvc;
using Postify.Modules.Profile.Core.Application.Commands;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Modules.Profile.Helpers;
using Postify.Modules.Profile.Validators;

namespace Postify.Modules.Profile.Controllers;

[ApiController]
[Route("api/profile/individual-profiles")]
public class IndividualProfileController : ControllerBase
{
    private readonly RegisterIndividualProfileCommandHandler _registerHandler;
    private readonly RegisterIndividualProfileRequestValidator _registerValidator;

    public IndividualProfileController(
        RegisterIndividualProfileCommandHandler registerHandler,
        RegisterIndividualProfileRequestValidator registerValidator)
    {
        _registerHandler = registerHandler;
        _registerValidator = registerValidator;
    }

    [HttpPost]
    public async Task<ActionResult<ProfileResponse>> Register([FromBody] RegisterIndividualProfileRequest request)
    {
        ValidationHelper.ValidateAndThrow(_registerValidator, request);

        var command = new RegisterIndividualProfileCommand(
            request.NationalId,
            request.FirstName,
            request.LastName,
            request.PhoneNumber
        );

        var result = await _registerHandler.HandleAsync(command);
        return Ok(result);
    }
}

