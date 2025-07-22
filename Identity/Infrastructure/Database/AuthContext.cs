using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=guideflow-identity;SearchPath=users;Username=postgres;Password=kiso;");

		base.OnConfiguring(optionsBuilder);
	}
}
