using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Infrastructure.Persistence;

namespace SK.CRM.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly CrmDbContext _context;
        public CustomerRepository(CrmDbContext context) : base(context) {
            _context = context;
        }

        public async Task<List<Customer>> GetAllProspectAsync()
        {
            return await _context.Customers.Include(y => y.Addresses).AsNoTracking().Where(x=>x.IsProspect == true).ToListAsync();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.Include(y => y.Addresses).AsNoTracking().Where(x => x.IsProspect == false).ToListAsync();
        }

        public async Task<bool> DisableCustomerAsync(Guid customerId)
        {
            int rowsAffected = _context.Customers
                    .Where(e => e.Id == customerId)
                    .ExecuteUpdate(setters => setters.SetProperty(e => e.IsDisabled, true));
            if(rowsAffected == 0) return false;
            return true;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetCustomerByUserIdAsync(string userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
