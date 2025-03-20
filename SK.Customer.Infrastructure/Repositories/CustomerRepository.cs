using SK.Customer.Application.Interfaces;
using SK.Customer.Infrastructure.Persistence;

namespace SK.Customer.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Domain.Entities.Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context) { }
    }
}
