using Api.Dto;
using AutoMapper;
using Core.Domain;

namespace Core.Mappers;

public class AuthProfile : Profile
{
	public AuthProfile()
	{
		CreateMap<RegistrationCredDto, User>();
		CreateMap<User, RegisteredUserDto>();
	}
}
