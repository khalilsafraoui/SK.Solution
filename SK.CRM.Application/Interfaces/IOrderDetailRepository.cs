using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Interfaces
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        // You can add custom methods specific to Order, if needed
        Task UpdateItemsAsync(Guid id, List<OrderDetail> items);
    }
}
