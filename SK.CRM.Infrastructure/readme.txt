entityframework cmd 
add-migration updatecustomerfields -context CrmDbContext -project SK.CRM.Infrastructure

update-database -context CrmDbContext -project SK.CRM.Infrastructure
Remove-Migration -Context CrmDbContext -project SK.CRM.Infrastructure


Pour PostgreSql
add-migration InitialPostgreMigration -context CrmDbContext -project SK.CRM.Infrastructure.PostgreSql
update-database -context CrmDbContext -project SK.CRM.Infrastructure.PostgreSql