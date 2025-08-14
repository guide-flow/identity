using Api.Dto;
using Api.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}
