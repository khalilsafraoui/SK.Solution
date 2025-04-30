using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Inventory.Infrastructure.PostgreSql;
using SK.Inventory.Infrastructure.SqlServer;


namespace SK.Inventory.UI.Blazor
{
    public static class InventoryRegistration
    {
        public static void AddInventoryModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["DatabaseProvider"];

            if (provider == "PostgreSql")
                services.AddPostgreSqlInfrastructure(configuration);
            else
                services.AddSqlInfrastructure(configuration);

        }
    }
}
