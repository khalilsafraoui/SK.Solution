using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Infrastructure.Persistence;

namespace SK.CRM.Infrastructure.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly CrmDbContext _context;
        public OrderDetailRepository(CrmDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateItemsAsync(Guid id, List<OrderDetail> items)
        {
            await DeleteRangeAsync(x => x.OrderId.Equals(id));
            await CreateRangeAsync(items);
        }
    }
}
