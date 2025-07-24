using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public bool IsBlocked { get; set; }

        public UserDto() { }

        public UserDto(int id, string username, string password, Role role, bool isBlocked)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            IsBlocked = isBlocked;
        }
    }
}
