using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Infrastructure.PostgreSql.Persistence;
using SK.Inventory.Infrastructure.PostgreSql.Repositories;

namespace SK.Inventory.Infrastructure.PostgreSql
{
    public static class PostgreSqlInventoryRegistration
    {
        public static IServiceCollection AddPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostGreSQLInentoryConnection")
                ?? throw new InvalidOperationException("Connection string 'PostGreSQLInventoryConnection' not found.");

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Repositories spécifiques PostgreSQL
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
