using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Visit.Application.Interfaces;
using SK.Visit.Infrastructure.PostgreSql.Persistence;
using SK.Visit.Infrastructure.PostgreSql.Repositories;

namespace SK.Visit.Infrastructure.PostgreSql
{
    public static class PostgreSqlVisitRegistration
    {
        public static IServiceCollection AddPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var keyVaultUriString = configuration["KeyVault:Uri"];
            var keyVaultConnectionString = configuration["KeyVault:VisitConnectionString"];
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

            services.AddDbContext<VisitDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Repositories spécifiques PostgreSQL
            services.AddScoped<IDestinationRepository, DestinationRepository>();
            

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
