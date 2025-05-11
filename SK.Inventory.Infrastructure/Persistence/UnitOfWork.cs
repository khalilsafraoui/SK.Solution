using SK.Inventory.Application.Interfaces;

namespace SK.Inventory.Infrastructure.SqlServer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;
        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }

        public UnitOfWork(
        InventoryDbContext context,
        ICategoryRepository categoryRepository,
        IProductRepository productRepository)
        {
            _context = context;
            Categories = categoryRepository;
            Products = productRepository;
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
