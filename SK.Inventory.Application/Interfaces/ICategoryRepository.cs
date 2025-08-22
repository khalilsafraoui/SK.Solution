using SK.Inventory.Domain.Entities.Product;

namespace SK.Inventory.Application.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>,
        IIntEntityRepository<Category>
    {
        // You can add custom methods specific to Customer, if needed
        Task<(List<Category> categories, int TotalCount)> GetAllAsync(int pageIndex = 0, int pageSize = 0);
    }
}
