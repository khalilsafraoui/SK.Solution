entityframework cmd 
always check appsettings.json for connection string and DatabaseProvider should be set to "SqlServer" or "Postgres" depending on the database you are using.

add-migration UserIdIsNoteRequiredInQuote -context SK.CRM.Infrastructure.Persistence.CrmDbContext -project SK.CRM.Infrastructure

update-database -context SK.CRM.Infrastructure.Persistence.CrmDbContext -project SK.CRM.Infrastructure
Remove-Migration -Context SK.CRM.Infrastructure.Persistence.CrmDbContext -project SK.CRM.Infrastructure

