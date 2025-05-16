using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plk.Blazor.DragDrop;
using SK.Visit.UI.Blazor.Services;

namespace SK.Visit.UI.Blazor
{
    public static class VisiRegistration
    {
        public static void AddVisitModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlazorDragDrop();
            services.AddScoped<IVisitPlanningServices, VisitPlanningServices>();
            services.AddSingleton<DestinationNotifier>();

        }
    }
}
