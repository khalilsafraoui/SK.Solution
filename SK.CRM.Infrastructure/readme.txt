entityframework cmd 
add-migration updatecustomerfields -context CrmDbContext
update-database -context CrmDbContext
Remove-Migration -Context CrmDbContext
