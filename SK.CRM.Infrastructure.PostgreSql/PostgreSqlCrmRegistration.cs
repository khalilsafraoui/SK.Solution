using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.CRM.Application.Interfaces;
using SK.CRM.Infrastructure.PostgreSql.Persistence;
using SK.CRM.Infrastructure.PostgreSql.Repositories;

namespace SK.CRM.Infrastructure.PostgreSql
{
    public static class PostgreSqlCrmRegistration
    {
        public static IServiceCollection AddPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostGreSQLCrmConnection")
                ?? throw new InvalidOperationException("Connection string 'PostGreSQLCrmConnection' not found.");

            services.AddDbContext<CrmDbContext>(options =>
                options.UseNpgsql(connectionString), ServiceLifetime.Transient);

            // Repositories spécifiques PostgreSQL
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
