using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterface;

public interface IAuthRepository
{
	Task<User> RegisterAsync(User user);
}
