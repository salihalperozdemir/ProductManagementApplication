using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.Models;

namespace ProductManagement_DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.EnableSensitiveDataLogging();
			optionsBuilder.UseSqlServer("Server=.;Database=ProductManagement;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}
