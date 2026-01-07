using Microsoft.AspNetCore.Mvc;
using Postify.Modules.Profile.Core.Application.Commands;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Modules.Profile.Helpers;
using Postify.Modules.Profile.Validators;

namespace Postify.Modules.Profile.Controllers;

[ApiController]
[Route("api/profile/corporate-profiles")]
public class CorporateProfileController : ControllerBase
{
    private readonly RegisterCorporateProfileCommandHandler _registerHandler;
    private readonly RegisterCorporateProfileRequestValidator _registerValidator;

    public CorporateProfileController(
        RegisterCorporateProfileCommandHandler registerHandler,
        RegisterCorporateProfileRequestValidator registerValidator)
    {
        _registerHandler = registerHandler;
        _registerValidator = registerValidator;
    }

    [HttpPost]
    public async Task<ActionResult<ProfileResponse>> Register([FromBody] RegisterCorporateProfileRequest request)
    {
        ValidationHelper.ValidateAndThrow(_registerValidator, request);

        var command = new RegisterCorporateProfileCommand(
            request.NationalId,
            request.Name,
            request.EconomicId
        );

        var result = await _registerHandler.HandleAsync(command);
        return Ok(result);
    }
}

