using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Identity.Application.Account;
using SK.Identity.Infrastructure.PostgreSql;
using SK.Identity.Infrastructure.SqlSerer;
using SK.Solution.Shared.Interfaces.Identity;



namespace SK.Identity.UI.Blazor
{
    public static class IdentityRegistration
    {
        public static void AddIdentityModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["DatabaseProvider"];

            if (provider == "PostgreSql")
                services.AddPostgreSqlInfrastructure(configuration);
            else
                services.AddSqlInfrastructure(configuration);
            services.AddScoped<ISharedUserServices, SharedUserServices>();

        }
    }
}
