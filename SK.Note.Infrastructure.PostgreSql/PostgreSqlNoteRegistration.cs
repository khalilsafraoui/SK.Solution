using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Note.Application.Interfaces;
using SK.Note.Infrastructure.PostgreSql.Persistence;
using SK.Note.Infrastructure.PostgreSql.Repositories;

namespace SK.Note.Infrastructure.PostgreSql
{
    public static class PostgreSqlNoteRegistration
    {
        public static IServiceCollection AddPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostGreSQLNoteConnection")
                ?? throw new InvalidOperationException("Connection string 'PostGreSQLNoteConnection' not found.");

            services.AddDbContext<NoteDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Repositories spécifiques PostgreSQL
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteCategoryRepository, NoteCategoryRepository>();

            // Générique
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
