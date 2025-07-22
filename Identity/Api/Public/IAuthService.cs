using Api.Dto;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Public;

public interface IAuthService
{
	Task<Result<RegisteredUserDto>> RegisterAsync(RegistrationCredDto creds);
}
