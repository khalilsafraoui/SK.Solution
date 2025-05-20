using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.Interfaces
{
    public interface IDestinationRepository : IGenericRepository<Destination>,
        IGuidEntityRepository<Destination>
    {
        Task<List<Destination>> SaveDestinationsAsync(List<Destination> newDestinations);

        Task<List<Destination>> GetDestinationsStartingFromSpecificDateAsync(DateTime date);

        Task<List<Destination>> GetDestinationsByAgentAndDayAsync(string AgentId, DateTime date);

        Task<Destination> UpdateArrivalTime(Destination destination);

        Task<Destination> UpdateTripCompleted(Destination destination);

        Task<Destination> UpdateTripSkippedTemporary(Destination destination);

        Task<Destination> UpdateTripSkippedPermanently(Destination destination);
    }
}
