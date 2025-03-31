using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Infrastructure.Persistence
{
    public class CrmDbContext : DbContext
    {
        public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Order>().HasKey(c => c.Id);
            modelBuilder.Entity<OrderDetail>().HasKey(c => c.Id);
            modelBuilder.Entity<Address>().HasKey(c => c.Id);
            modelBuilder.Entity<Address>()
                        .Property(a => a.Latitude)
                        .HasColumnType("decimal(9,6)");

            modelBuilder.Entity<Address>()
                        .Property(a => a.Longitude)
                        .HasColumnType("decimal(9,6)");
        }
    }
}
