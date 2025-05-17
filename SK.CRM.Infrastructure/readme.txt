entityframework cmd 
add-migration AddCustomerIdAndAddressId -context CrmDbContext -project SK.CRM.Infrastructure

update-database -context CrmDbContext -project SK.CRM.Infrastructure
Remove-Migration -Context CrmDbContext -project SK.CRM.Infrastructure


Pour PostgreSql
add-migration AddCustomerIdAndAddressId -context CrmDbContext -project SK.CRM.Infrastructure.PostgreSql
update-database -context CrmDbContext -project SK.CRM.Infrastructure.PostgreSql