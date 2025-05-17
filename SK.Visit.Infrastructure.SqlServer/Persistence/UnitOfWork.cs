using SK.Visit.Application.Interfaces;

namespace SK.Visit.Infrastructure.SqlServer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VisitDbContext _context;
        public IDestinationRepository DestinationsRepository { get; private set; }
       

        public UnitOfWork(
        VisitDbContext context,
        IDestinationRepository destinationRepository)
        {
            _context = context;

            DestinationsRepository = destinationRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
