using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectVaila.Providers.EFCoreProvider.Data;

namespace ProjectVaila.Providers.EFCoreProvider
{
	public class AppDbContext : IdentityDbContext<IdentityUserContext>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<IdentityUserContext> IdentityUserContexts { get; private set; }
		public DbSet<User> Users { get; private set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(u => u.Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}
