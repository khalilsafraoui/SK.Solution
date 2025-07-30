using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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
            var keyVaultUriString = configuration["KeyVault:Uri"];
            var keyVaultConnectionString = configuration["KeyVault:CrmConnectionString"];
            if (string.IsNullOrWhiteSpace(keyVaultUriString))
                throw new InvalidOperationException("Key Vault URI not configured.");

            var keyVaultUri = new Uri(keyVaultUriString);
            var secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential());

            KeyVaultSecret dbSecret;
            try
            {
                dbSecret = secretClient.GetSecret(keyVaultConnectionString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not retrieve PostgreSQL connection string from Key Vault.", ex);
            }

            var connectionString = dbSecret?.Value;
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string is null or empty.");

            services.AddDbContext<CrmDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Repositories spécifiques PostgreSQL
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IQuoteItemRepository, QuoteItemRepository>();

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
