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
		if (creds.Password != creds.ConfirmPassword) return Result.Fail("Passwords do not match");

		var user = _mapper.Map<User>(creds);
		user.HashPassword();

        await _authRepository.RegisterAsync(user);

		return _mapper.Map<RegisteredUserDto>(user);
	}

	public async Task<Result<AuthenticationResponseDto>> AuthenticateAsync(AuthenticationRequestDto auth)
	{
		var user = await _authRepository.GetByUsernameAsync(auth.Username);
		if (user is null) return Result.Fail("User not found");

		if (!user.VerifyPassword(auth.Password)) return Result.Fail("Invalid password");

        return new AuthenticationResponseDto(auth.Username, GenerateAccessToken(user));
	}

	private static string GenerateAccessToken(User user)
	{
		var claims = new List<Claim> {
			new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new(JwtRegisteredClaimNames.Email, user.Username),
			new("role", user.Role.ToString()),
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
