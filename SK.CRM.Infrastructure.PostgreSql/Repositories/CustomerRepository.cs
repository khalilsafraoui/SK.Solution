using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Infrastructure.PostgreSql.Persistence;

namespace SK.CRM.Infrastructure.PostgreSql.Repositories
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

        public async Task<(List<Customer> Items, int TotalCount)> GetAllProspectAsync(
            int pageIndex = 0,
            int pageSize = 10,
            bool include = false,
            bool noTracking = false)
        {
            var query = _context.Customers.Where(c => c.IsProspect);

            if (include)
                query = query.Include(c => c.Addresses);

            if (noTracking)
                query = query.AsNoTracking();

            var totalCount = await query.CountAsync();

            // Apply pagination only if pageSize > 0
            if (pageSize > 0)
            {
                query = query
                    .OrderByDescending(c => c.CreatedDate)
                    .ThenBy(c => c.Id)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
            }

            var items = await query.ToListAsync();

            return (items, totalCount);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.Include(y => y.Addresses).AsNoTracking().Where(x => x.IsProspect == false).ToListAsync();
        }

        public async Task<(List<Customer> Items, int TotalCount)> GetAllCustomersAsync(
            int pageIndex = 0,
            int pageSize = 10,
            bool include = false,
            bool noTracking = false)
        {
            var query = _context.Customers.Where(c => !c.IsProspect);

            if (include)
                query = query.Include(c => c.Addresses);

            if (noTracking)
                query = query.AsNoTracking();

            var totalCount = await query.CountAsync();

            // Apply pagination only if pageSize > 0
            if (pageSize > 0)
            {
                query = query
                    .OrderByDescending(c => c.CreatedDate)
                    .ThenBy(c => c.Id)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
            }

            var items = await query.ToListAsync();

            return (items, totalCount);
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
