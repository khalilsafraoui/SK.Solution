using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Customer.Application.Interfaces;
using SK.Customer.Infrastructure.Persistence;
using SK.Customer.Infrastructure.Repositories;

namespace SK.Customer.UI.Blazor
{
    public static class CustomerRegistration
    {
        public static void AddCustomerModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register Specific Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Database Configuration
            var customerConnectionString = configuration.GetConnectionString("CustomerConnection") ?? throw new InvalidOperationException("Connection string 'CustomerConnection' not found.");
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(customerConnectionString));
        }
    }
}
