using Api.Dto;
using Api.Public;
using AutoMapper;
using Common;
using Core.Domain;
using Core.Domain.RepositoryInterface;
using FluentResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.UseCase;

public class AuthService : IAuthService
{
	private readonly IMapper _mapper;
	private readonly IAuthRepository _authRepository;

	public AuthService(IMapper mapper, IAuthRepository authRepository)
	{
		_mapper = mapper;
		_authRepository = authRepository;
	}

	public async Task<Result<RegisteredUserDto>> RegisterAsync(RegistrationCredDto creds)
	{
		var registeredUser = await _authRepository.RegisterAsync(_mapper.Map<User>(creds));

		return _mapper.Map<RegisteredUserDto>(registeredUser);
	}

	public async Task<Result<AuthenticationResponseDto>> AuthenticateAsync(AuthenticationRequestDto auth)
	{
		// When implemented
		// var userResult = await user/stakeholderService.GetByUsernameAsync(auth.Username);
		await Task.Delay(200);

		return new AuthenticationResponseDto(auth.Username, GenerateAccessToken(auth.Username));
	}

	// When user fetched, more claims could be attached
	private string GenerateAccessToken(string username)
	{
		var claims = new List<Claim> {
			new(JwtRegisteredClaimNames.Sub, username)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetSecretKey()));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: Config.GetIssuer(),
			audience: Config.GetAudience(),
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.UtcNow.AddHours(6),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
