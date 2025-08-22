using Microsoft.EntityFrameworkCore;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;
using SK.Inventory.Infrastructure.SqlServer.Persistence;

namespace SK.Inventory.Infrastructure.SqlServer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly InventoryDbContext _context;
        
        public ProductRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }
       
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<(List<Product> products, int TotalCount)> GetAllAsync(int pageIndex = 0, int pageSize = 0)
        {
            var query = _context.Products.Include(x => x.Category).AsNoTracking();

            var totalCount = await query.CountAsync();

            if (pageSize != 0)
            {
                var pagedProducts = await query
               .OrderBy(q => q.Id) // Always order before skip/take
               .Skip(pageIndex * pageSize)
               .Take(pageSize)
               .ToListAsync();
                return (pagedProducts, totalCount);
            }

            return (await query.OrderBy(q => q.Id).ToListAsync(), totalCount);
        }

        public async Task<Product?> GetByIdAsync(int Id)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productResult = await _context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (productResult is null)
            {
                throw new NotFoundException(nameof(Product), product.Id);
            }
            
                productResult.Name = product.Name;
                productResult.Price = product.Price;
                productResult.Description = product.Description;
                productResult.SpecialTag = product.SpecialTag;
                productResult.CategoryId = product.CategoryId;
                productResult.ImageUrl = product.ImageUrl;
                _context.Products.Update(productResult);
                return productResult;
           
        }
    }
}
