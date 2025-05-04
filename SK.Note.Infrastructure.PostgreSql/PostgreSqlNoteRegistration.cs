using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Note.Application.Interfaces;
using SK.Note.Infrastructure.PostgreSql.Persistence;
using SK.Note.Infrastructure.PostgreSql.Repositories;

namespace SK.Note.Infrastructure.PostgreSql
{
    public static class PostgreSqlNoteRegistration
    {
        public static IServiceCollection AddPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var keyVaultUriString = configuration["KeyVault:Uri"];
            var keyVaultConnectionString = configuration["KeyVault:NoteConnectionString"];
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

            services.AddDbContext<NoteDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Repositories spécifiques PostgreSQL
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteCategoryRepository, NoteCategoryRepository>();

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
