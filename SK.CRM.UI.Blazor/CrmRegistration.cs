using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.CRM.Infrastructure;
using SK.CRM.Infrastructure.PostgreSql;
using SK.CRM.UI.Blazor.Services;

namespace SK.CRM.UI.Blazor
{
    public static class CrmRegistration
    {
        public static void AddCrmModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["DatabaseProvider"];

            if (provider == "PostgreSql")
                services.AddPostgreSqlInfrastructure(configuration);
            else
                services.AddSqlInfrastructure(configuration);

            services.AddSingleton<SharedStateService>();
            services.AddScoped<PaymentService>();
        }
    }
}
