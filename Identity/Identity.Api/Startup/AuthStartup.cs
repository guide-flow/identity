using Api.Public;
using Core.Domain.RepositoryInterface;
using Core.Mappers;
using Core.UseCase;
using Infrastructure.Database.Repository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Common;

namespace Identity.Api.Startup;

public static class AuthStartup
{
	public static IServiceCollection SetupAuth(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(AuthProfile).Assembly);

		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService,UserService>();

		services.AddScoped<IAuthRepository, AuthRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		services.AddDbContext<AuthContext>(opt =>
			opt.UseNpgsql(Config.GetDbConnectionString(), x => x.MigrationsHistoryTable("__EFMigrationsHistory", "users"))
		);

		return services;
	}
}
