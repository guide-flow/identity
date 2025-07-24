using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterface
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task Update(User user); 
        Task<List<User>> GetAll();
    }
}
