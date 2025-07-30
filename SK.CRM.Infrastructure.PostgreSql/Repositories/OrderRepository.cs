using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Domain.Entities.Quote;
using SK.CRM.Infrastructure.PostgreSql.Persistence;


namespace SK.CRM.Infrastructure.PostgreSql.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly CrmDbContext _context;
        public OrderRepository(CrmDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string? userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            }
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public Task<Order> GetBySessionIdAsync(string sessionId)
        {
            return _context.Orders.FirstOrDefaultAsync(o => o.SessionId == sessionId);
        }

        public async Task<Order> UpdateStatusAsync(Guid orderId, string status, string paymentIntentId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    order.PaymentIntentId = paymentIntentId;
                }
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                return await _context.Orders.Where(o => o.Status == status).ToListAsync();
            }
            return await _context.Orders.ToListAsync();
        }

        public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetByStatusesAsync(
            IEnumerable<string> statuses,
            int pageIndex,
            int pageSize,
            string? userId = null)
        {
            var query = _context.Orders.AsNoTracking()
                .Where(q => statuses.Contains(q.Status));

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(q => q.UserId == userId);
            }

            var totalCount = await query.CountAsync();

            var PagedOrders = await query
                .OrderBy(q => q.Id) // Always order before skip/take
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (PagedOrders, totalCount);
        }
    }
}
