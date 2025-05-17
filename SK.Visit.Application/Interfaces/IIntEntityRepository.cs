using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.Interfaces
{
    public interface IIntEntityRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
    }
}
