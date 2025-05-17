using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.Interfaces
{
    public interface IGuidEntityRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
    }
}
