using Api.Dto;
using Api.Public;
using AutoMapper;
using Core.Domain;
using Core.Domain.RepositoryInterface;
using FluentResults;

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
}
