using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.CRM.Application.Interfaces;
using SK.CRM.Infrastructure.Persistence;
using SK.CRM.Infrastructure.Repositories;

namespace SK.CRM.UI.Blazor
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
            services.AddDbContext<CrmDbContext>(options =>
                options.UseSqlServer(customerConnectionString));
        }
    }
}
