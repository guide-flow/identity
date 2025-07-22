using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dto;

public record RegistrationCredDto(string Username, string Password, string ConfirmPassword, Role Role);
