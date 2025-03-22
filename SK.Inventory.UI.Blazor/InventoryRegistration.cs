using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Infrastructure.Persistence;
using SK.Inventory.Infrastructure.Repositories;


namespace SK.Inventory.UI.Blazor
{
    public static class InventoryRegistration
    {
        public static void AddInventoryModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register Specific Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            // Database Configuration
            var inventoryConnectionString = configuration.GetConnectionString("InventoryConnection") ?? throw new InvalidOperationException("Connection string 'InventoryConnection' not found.");
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(inventoryConnectionString));
        }
    }
}
