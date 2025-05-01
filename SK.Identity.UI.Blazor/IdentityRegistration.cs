using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Identity.Infrastructure.PostgreSql;
using SK.Identity.Infrastructure.SqlSerer;



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

        }
    }
}
