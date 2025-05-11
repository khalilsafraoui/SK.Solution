namespace SK.Inventory.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        // Add other repositories

        Task<int> SaveChangesAsync();
    }

}
