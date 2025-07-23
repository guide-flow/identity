using Api.Dto;
using Api.Public;
using AutoMapper;
using Core.Domain.RepositoryInterface;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCase
{
    public class UserService:IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> BlockUser(int id)
        {
            try
            {
                var user = await _userRepository.GetById(id);

                user.BlockUser();

                await _userRepository.Update(user);

                var userDto = _mapper.Map<UserDto>(user);
                return Result.Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                return Result.Fail<UserDto>($"User with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                return Result.Fail<UserDto>($"An error occurred: {ex.Message}");
            }
        }
    }
}
