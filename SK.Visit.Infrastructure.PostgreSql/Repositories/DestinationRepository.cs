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
    }
}
