using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.CRM.Application.Interfaces;
using SK.CRM.Infrastructure.Persistence;
using SK.CRM.Infrastructure.Repositories;

namespace SK.CRM.Infrastructure
{
    public static class SqlCrmRegistration
    {
        public static IServiceCollection AddSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var CRMConnectionString = configuration.GetConnectionString("CrmConnection") ?? throw new InvalidOperationException("Connection string 'CrmConnection' not found.");

            services.AddDbContext<CrmDbContext>(options =>
               options.UseSqlServer(CRMConnectionString), ServiceLifetime.Transient);

            // Repositories spécifiques SQL Server
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
