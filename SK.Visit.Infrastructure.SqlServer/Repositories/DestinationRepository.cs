using Microsoft.EntityFrameworkCore;
using SK.Visit.Application.Interfaces;
using SK.Visit.Domain.Entities;
using SK.Visit.Domain.Enum;
using SK.Visit.Infrastructure.SqlServer.Persistence;

namespace SK.Visit.Infrastructure.SqlServer.Repositories
{
    public class DestinationRepository : GenericRepository<Destination>, IDestinationRepository
    {
        private readonly VisitDbContext _context;

        public DestinationRepository(VisitDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Destination>> SaveDestinationsAsync(List<Destination> newDestinations, DateTime cutoffDate)
        {
            // Step 1: Find existing future destinations
            var destinationsToDelete = await _context.Destinations
                .Where(d => d.Date >= cutoffDate)
                .ToListAsync();

            if (destinationsToDelete.Any())
            {
                _context.Destinations.RemoveRange(destinationsToDelete);
            }

            if (newDestinations.Any())
            {
                // Step 2: Add new destinations
                await _context.Destinations.AddRangeAsync(newDestinations);
            }
            
            // Step 3: Return the added destinations (note: IDs may still be 0 until SaveChangesAsync is called)
            return newDestinations;
        }

        public async Task<List<Destination>> GetDestinationsStartingFromSpecificDateAsync(DateTime date)
        {
            return await _context.Destinations
                .Where(d => d.Date >= date)
                .ToListAsync();
        }

        public async Task<List<Destination>> GetDestinationsByAgentAndDayAsync(string AgentId, DateTime date)
        {
            return await _context.Destinations
                .Where(d => d.Date == date && d.AgentId == AgentId)
                .ToListAsync();
        }

        public async Task<Destination> UpdateArrivalTime(Destination destination)
        {
            var destinationToUpdate = await _context.Destinations.FindAsync(destination.Id);
            if (destinationToUpdate != null)
            {
                destinationToUpdate.ArrivalTime = destination.ArrivalTime;
                destinationToUpdate.Status = TripStatus.Ongoing;
            }
            return destinationToUpdate;
        }

        public async Task<Destination> UpdateTripCompleted(Destination destination)
        {
            var destinationToUpdate = await _context.Destinations.FindAsync(destination.Id);
            if (destinationToUpdate != null)
            {
                destinationToUpdate.FinishTime = destination.FinishTime;
                destinationToUpdate.Status = TripStatus.Completed;
                if (destination.Note != null)
                {
                    destinationToUpdate.Note = destination.Note;
                }
            }
            return destinationToUpdate;
        }

        public async Task<Destination> UpdateTripSkippedTemporary(Destination destination)
        {
            var destinationToUpdate = await _context.Destinations.FindAsync(destination.Id);
            if (destinationToUpdate != null)
            {

                destinationToUpdate.Status = TripStatus.SkippedTemporary;
                destinationToUpdate.SkipReason = destination.SkipReason;
                if (destination.Note != null)
                {
                    destinationToUpdate.Note = destination.Note;
                }
            }
            return destinationToUpdate;
        }

        public async Task<Destination> UpdateTripSkippedPermanently(Destination destination)
        {
            var destinationToUpdate = await _context.Destinations.FindAsync(destination.Id);
            if (destinationToUpdate != null)
            {

                destinationToUpdate.Status = TripStatus.SkippedPermanently;
                destinationToUpdate.SkipReason = destination.SkipReason;
                if (destination.Note != null)
                {
                    destinationToUpdate.Note = destination.Note;
                }
                
            }
            return destinationToUpdate;
        }
    }
}
