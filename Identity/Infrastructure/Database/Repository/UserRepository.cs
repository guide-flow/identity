using Core.Domain;
using Core.Domain.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly AuthContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(AuthContext context)
        {
            _context = context;
            _users = _context.Set<User>();
        }
        public async Task<User> GetById(int id)
        {
            var user = await _users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }

            return user;
        }

        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("The user data was updated by another process.");
            }
        }
    }
}
