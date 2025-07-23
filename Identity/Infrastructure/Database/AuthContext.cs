using Common;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class AuthContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("users");

		base.OnModelCreating(modelBuilder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(Config.GetDbConnectionString());

		base.OnConfiguring(optionsBuilder);
	}
}
