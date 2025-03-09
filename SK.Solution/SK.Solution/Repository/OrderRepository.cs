using Microsoft.EntityFrameworkCore;
using SK.Solution.Data;
using SK.Solution.Data.Entities;
using SK.Solution.Repository.IRepository;

namespace SK.Solution.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            order.OrderDate = DateTime.Now;
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

        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public Task<Order> GetBySessionIdAsync(string sessionId)
        {
            return _context.Orders.FirstOrDefaultAsync(o => o.SessionId == sessionId);
        }

        public async Task<Order> UpdateStatusAsync(int orderId, string status, string paymentIntentId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
                if(!string.IsNullOrEmpty(paymentIntentId))
                {
                    order.PaymentIntentId = paymentIntentId;
                }
                await _context.SaveChangesAsync();
            }
            return order;
        }
    }
}
