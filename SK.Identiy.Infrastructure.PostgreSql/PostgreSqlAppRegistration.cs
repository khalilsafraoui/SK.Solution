using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Identity.Account;
using SK.Identity.Domain.Entities;
using SK.Identity.Infrastructure.PostgreSql.Persistence;


namespace SK.Identity.Infrastructure.PostgreSql
{
    public static class PostgreSqlAppRegistration
    {
        public static IServiceCollection AddPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var keyVaultUriString = configuration["KeyVault:Uri"];
            var keyVaultConnectionString = configuration["KeyVault:AppConnectionString"];
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

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString), ServiceLifetime.Transient);

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

            services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
            return services;
        }
    }
}
