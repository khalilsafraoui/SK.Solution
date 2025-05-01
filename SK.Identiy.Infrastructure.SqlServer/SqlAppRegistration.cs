using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Identity.Domain.Entities;
using SK.Identity.Infrastructure.SqlSerer.Persistence;
using SK.Identity.Account;
using Microsoft.AspNetCore.Components.Authorization;


namespace SK.Identity.Infrastructure.SqlSerer
{
    public static class SqlAppRegistration
    {
        public static IServiceCollection AddSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient);
            
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
