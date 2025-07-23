using Core.Domain;
using Core.Domain.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository;

public class AuthRepository : IAuthRepository
{
	private readonly AuthContext _context;
	private readonly DbSet<User> _users;

	public AuthRepository(AuthContext context)
	{
		_context = context;
		_users = _context.Set<User>();
	}

	public async Task<User> RegisterAsync(User user)
	{
		_users.Add(user);
		await _context.SaveChangesAsync();
		return user;
	}
}
