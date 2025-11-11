using Api.Dto;
using Api.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Api.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthenticationController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("register")]
	public async Task<ActionResult<RegisteredUserDto>> Register([FromBody] RegistrationCredDto creds)
	{
		// Prevent admin role creation through registration
		if (creds.Role == Common.Domain.Role.Admin)
		{
			return BadRequest("Admin accounts cannot be created through registration.");
		}

		var result = await _authService.RegisterAsync(creds);

		// Todo: Route not implemented
		return Created($"http://localhost:5226/api/users/{result.Value.Id}", result.Value);
	}

	[HttpPost("authenticate")]
	public async Task<ActionResult<AuthenticationResponseDto>> Authenticate([FromBody] AuthenticationRequestDto auth)
	{
		var result = await _authService.AuthenticateAsync(auth);
		if (result.IsFailed) return BadRequest(result.Errors.FirstOrDefault()?.Message);

        return Ok(result.Value);
	}

	[Authorize]
	[HttpGet("validate-token")]
	public ActionResult ValidateToken()
	{
        var username = User.FindFirstValue(ClaimTypes.Email);
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var role = User.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(username))
        {
            return Unauthorized("Invalid token");
        }

        return Ok(new
        {
            sub = id,
            email = username,
            role = role
        });
    }
}
