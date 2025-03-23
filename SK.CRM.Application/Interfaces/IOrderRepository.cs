using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // You can add custom methods specific to Order, if needed
        public Task<Order> GetByIdAsync(Guid orderId);
        public Task<Order> GetBySessionIdAsync(string sessionId);
        public Task<IEnumerable<Order>> GetAllAsync(string? userId = null);
        public Task<Order> CreateAsync(Order order);
        public Task<Order> UpdateStatusAsync(Guid orderId, string status, string paymentIntentId);
    }
}
