using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Visit.Application.Interfaces;
using SK.Visit.Infrastructure.SqlServer.Persistence;
using SK.Visit.Infrastructure.SqlServer.Repositories;

namespace SK.Visit.Infrastructure.SqlServer
{
    public static class SqlVisitRegistration
    {
        public static IServiceCollection AddSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var inventoryConnectionString = configuration.GetConnectionString("VisitConnection") ?? throw new InvalidOperationException("Connection string 'VisitConnection' not found.");
            services.AddDbContext<VisitDbContext>(options =>
                options.UseSqlServer(inventoryConnectionString));

            // Repositories spécifiques SQL Server
            services.AddScoped<IDestinationRepository, DestinationRepository>();
           

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGuidEntityRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IIntEntityRepository<>), typeof(GenericRepository<>));
            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
