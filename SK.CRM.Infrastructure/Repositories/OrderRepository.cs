using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Infrastructure.Persistence;


namespace SK.CRM.Infrastructure.Repositories
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
            await _context.SaveChangesAsync();
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

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                return await _context.Orders.Where(o => o.Status == status).ToListAsync();
            }
            return await _context.Orders.ToListAsync();
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
                await _context.SaveChangesAsync();
            }
            return order;
        }
    }
}
