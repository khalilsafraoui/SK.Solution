using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Note.Application.Interfaces;
using SK.Note.Infrastructure.SqlServer.Persistence;
using SK.Note.Infrastructure.SqlServer.Repositories;

namespace SK.Note.Infrastructure.SqlServer
{
    public static class SqlNoteRegistration
    {
        public static IServiceCollection AddSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var inventoryConnectionString = configuration.GetConnectionString("InventoryConnection") ?? throw new InvalidOperationException("Connection string 'InventoryConnection' not found.");
            services.AddDbContext<NoteDbContext>(options =>
                options.UseSqlServer(inventoryConnectionString));

            // Repositories spécifiques SQL Server
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteCategoryRepository, NoteCategoryRepository>();

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
