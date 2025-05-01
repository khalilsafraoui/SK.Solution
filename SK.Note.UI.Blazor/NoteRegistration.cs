using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SK.Note.Infrastructure.PostgreSql;
using SK.Note.Infrastructure.SqlServer;


namespace SK.Inventory.UI.Blazor
{
    public static class NoteRegistration
    {
        public static void AddNoteModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["DatabaseProvider"];

            if (provider == "PostgreSql")
                services.AddPostgreSqlInfrastructure(configuration);
            else
                services.AddSqlInfrastructure(configuration);

        }
    }
}
