using SK.Inventory.Domain.Entities;

namespace SK.Inventory.Application.Interfaces
{
    public interface IIntEntityRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
    }
}
