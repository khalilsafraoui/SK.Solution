using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Infrastructure.Persistence;

namespace SK.CRM.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CrmDbContext context) : base(context) { }
    }
}
