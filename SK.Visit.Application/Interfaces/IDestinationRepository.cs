using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.Interfaces
{
    public interface IDestinationRepository : IGenericRepository<Destination>,
        IGuidEntityRepository<Destination>
    {
        Task<List<Destination>> SaveDestinationsAsync(List<Destination> newDestinations);

        Task<List<Destination>> GetDestinationsStartingFromTomorrowAsync();
    }
}
