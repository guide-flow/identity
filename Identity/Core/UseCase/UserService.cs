using Api.Dto;
using Api.Public;
using AutoMapper;
using Core.Domain.RepositoryInterface;
using FluentResults;

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

        public async Task<Result<List<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return Result.Ok(_mapper.Map<List<UserDto>>(users));
        }
    }
}
