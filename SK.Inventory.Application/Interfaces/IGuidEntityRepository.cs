using SK.Inventory.Domain.Entities;

namespace SK.Inventory.Application.Interfaces
{
    public interface IGuidEntityRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
