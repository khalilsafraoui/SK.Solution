using SK.Solution.Shared.Interfaces.Crm;
using SK.Solution.Shared.Interfaces.Identity;
using SK.Visit.Application.Interfaces;

namespace SK.Visit.Infrastructure.PostgreSql.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VisitDbContext _context;
        public IDestinationRepository DestinationsRepository { get; private set; }

        public ISharedOrderServices SharedOrderServices { get; private set; }

        public ISharedUserServices SharedUserServices { get; private set; }

        public ISharedCustomerServices SharedCustomerServices { get; private set; }

        public UnitOfWork(
        VisitDbContext context,
        IDestinationRepository destinationRepository, ISharedOrderServices sharedOrderServices, ISharedUserServices sharedUserServices, ISharedCustomerServices sharedCustomerServices)
        {
            _context = context;

            DestinationsRepository = destinationRepository;
            SharedOrderServices = sharedOrderServices;
            SharedUserServices = sharedUserServices;
            SharedCustomerServices = sharedCustomerServices;
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
