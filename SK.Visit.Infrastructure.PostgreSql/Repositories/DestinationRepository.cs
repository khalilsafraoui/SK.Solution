using Microsoft.EntityFrameworkCore;
using SK.Visit.Application.Interfaces;
using SK.Visit.Domain.Entities;
using SK.Visit.Infrastructure.PostgreSql.Persistence;

namespace SK.Visit.Infrastructure.PostgreSql.Repositories
{
    public class DestinationRepository : GenericRepository<Destination>, IDestinationRepository
    {
        private readonly VisitDbContext _context;

        public DestinationRepository(VisitDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Destination>> SaveDestinationsAsync(List<Destination> newDestinations)
        {
            var cutoffDate = DateTime.Today.AddDays(1);

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

        public async Task<List<Destination>> GetDestinationsStartingFromTomorrowAsync()
        {
            var tomorrow = DateTime.Today.AddDays(1);

            return await _context.Destinations
                .Where(d => d.Date >= tomorrow)
                .ToListAsync();
        }

    }
}
