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
        public Role Role { get; set; }
        public bool IsBlocked { get; set; }

        public UserDto() { }

        public UserDto(int id, string username, Role role, bool isBlocked)
        {
            Id = id;
            Username = username;
            Role = role;
            IsBlocked = isBlocked;
        }
    }
}
