namespace SK.Visit.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDestinationRepository DestinationsRepository { get; }
        
        // Add other repositories

        Task<int> SaveChangesAsync();
    }

}
