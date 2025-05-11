using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Infrastructure.SqlServer.Persistence;
using SK.Inventory.Infrastructure.SqlServer.Repositories;

namespace SK.Inventory.Infrastructure.SqlServer
{
    public static class SqlInventoryRegistration
    {
        public static IServiceCollection AddSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var inventoryConnectionString = configuration.GetConnectionString("InventoryConnection") ?? throw new InvalidOperationException("Connection string 'InventoryConnection' not found.");
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(inventoryConnectionString));

            // Repositories spécifiques SQL Server
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGuidEntityRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IIntEntityRepository<>), typeof(GenericRepository<>));
            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
