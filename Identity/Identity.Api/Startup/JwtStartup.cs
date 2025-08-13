using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Api.Startup;

public static class JwtStartup
{
	public static IServiceCollection AddJwtAuth(this IServiceCollection services)
	{
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = Config.GetIssuer(),
					ValidAudience = Config.GetAudience(),
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetSecretKey()))
				};
			});

		return services;
	}
}
