using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.Interfaces
{
    public interface IDestinationRepository : IGenericRepository<Destination>,
        IGuidEntityRepository<Destination>
    {
        // You can add custom methods specific to Customer, if needed
    }
}
