using SK.Solution.Shared.Interfaces.Crm;
using SK.Solution.Shared.Interfaces.Identity;

namespace SK.Visit.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDestinationRepository DestinationsRepository { get; }

        ISharedOrderServices SharedOrderServices { get; }

        ISharedUserServices SharedUserServices { get; }

        ISharedCustomerServices SharedCustomerServices { get; }

        // Add other repositories

        Task<int> SaveChangesAsync();
    }

}
