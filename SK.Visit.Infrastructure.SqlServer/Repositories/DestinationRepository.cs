using SK.Visit.Application.Interfaces;
using SK.Visit.Domain.Entities;
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
    }
}
