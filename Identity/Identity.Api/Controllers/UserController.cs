using Api.Dto;
using Api.Public;
using Core.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Policy = "administratorPolicy")]
        [HttpGet()]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var result = await _userService.GetAll();

            return Ok(result.Value);
        }

        //[Authorize(Policy = "administratorPolicy")]
        [HttpPut("block/{id}")]
        public async Task<ActionResult<UserDto>> BlockUser(int id)
        {
            var result = await _userService.BlockUser(id);

            if (result.IsFailed)
            {
                return NotFound(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }
    }
}
