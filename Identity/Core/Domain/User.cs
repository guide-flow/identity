using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain;

public class User
{
	public int Id { get; init; }
	public string Username { get; init; }
	public string Password { get; init; }
	public Role Role { get; private set; }
	public bool IsBlocked { get; private set; } 

	public User(string username, string password, Role role, bool isBlocked = false)
	{
		Username = username;
		Password = password;
		Role = role;
		IsBlocked = isBlocked;
	}

	public void BlockUser()
	{
		IsBlocked = true;
	}
}