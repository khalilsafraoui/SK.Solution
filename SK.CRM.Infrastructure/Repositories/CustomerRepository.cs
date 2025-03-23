using SK.CRM.Application.Interfaces;
using SK.CRM.Infrastructure.Persistence;

namespace SK.CRM.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Domain.Entities.Customer>, ICustomerRepository
    {
        public CustomerRepository(CrmDbContext context) : base(context) { }
    }
}
