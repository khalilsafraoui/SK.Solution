using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Inventory.Application.Interfaces.Common;
using SK.Inventory.Infrastructure.PostgreSql;
using SK.Inventory.Infrastructure.SqlServer;
using SK.Inventory.UI.Blazor.Common;


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

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

        }
    }
}
