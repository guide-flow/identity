using Common.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain;

[DebuggerDisplay("{Username}, {Role}")]
public class User
{
	public int Id { get; init; }
	public string Username { get; init; }

    /// <summary>
    /// Gets or sets the HASHED password for the user.
    /// </summary>
    public string Password { get; private set; }
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

	public void HashPassword()
	{
		Password = BCrypt.Net.BCrypt.HashPassword(Password);
    }

	public bool VerifyPassword(string password)
	{
		return BCrypt.Net.BCrypt.Verify(password, Password);
    }
}