using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(InventoryDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
       

        public async Task<bool> DeleteAsync(int Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);

           
            if (product != null)
            {
                if(!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
               
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
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
                await _context.SaveChangesAsync();
                return productResult;
           
        }
    }
}
