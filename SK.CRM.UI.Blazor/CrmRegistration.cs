using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.CRM.Application.Interfaces;
using SK.CRM.Infrastructure.Persistence;
using SK.CRM.Infrastructure.Repositories;
using SK.CRM.UI.Blazor.Services;

namespace SK.CRM.UI.Blazor
{
    public static class CrmRegistration
    {
        public static void AddCrmModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register Specific Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<SharedStateService>();
            services.AddScoped<PaymentService>();
            // Database Configuration
            var customerConnectionString = configuration.GetConnectionString("CrmConnection") ?? throw new InvalidOperationException("Connection string 'CrmConnection' not found.");
            services.AddDbContext<CrmDbContext>(options =>
                options.UseSqlServer(customerConnectionString), ServiceLifetime.Transient);
        }
    }
}
