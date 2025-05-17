using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plk.Blazor.DragDrop;
using SK.Visit.Application.Interfaces.Common;
using SK.Visit.Infrastructure.PostgreSql;
using SK.Visit.Infrastructure.SqlServer;
using SK.Visit.UI.Blazor.Common;
using SK.Visit.UI.Blazor.Services;

namespace SK.Visit.UI.Blazor
{
    public static class VisiRegistration
    {
        public static void AddVisitModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["DatabaseProvider"];

            if (provider == "PostgreSql")
                services.AddPostgreSqlInfrastructure(configuration);
            else
                services.AddSqlInfrastructure(configuration);

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddBlazorDragDrop();
            services.AddScoped<IVisitPlanningServices, VisitPlanningServices>();
            services.AddSingleton<DestinationNotifier>();

        }
    }
}
