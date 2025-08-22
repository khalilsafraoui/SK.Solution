using Microsoft.EntityFrameworkCore;
using SK.Inventory.Application.Interfaces.Common;
using SK.Inventory.Domain.Entities;
using SK.Inventory.Domain.Entities.Product;
using SK.Inventory.Infrastructure.SqlServer.Configuration.Products;

namespace SK.Inventory.Infrastructure.SqlServer.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        private readonly ICurrentUserService? _currentUserService;

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = await _currentUserService?.GetUserIdAsync();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = await _currentUserService?.GetUserIdAsync();
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
