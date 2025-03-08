using SK.Solution.Data.Entities;

namespace SK.Solution.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<Order> GetByIdAsync(int orderId);
        public Task<IEnumerable<Order>> GetAllAsync(string? userId=null);
        public Task<Order> CreateAsync(Order order);
        public Task<Order> UpdateStatusAsync(int orderId,string status);
    }
}
