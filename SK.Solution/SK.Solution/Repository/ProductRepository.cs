using Microsoft.EntityFrameworkCore;
using SK.Solution.Data;
using SK.Solution.Data.Entities;
using SK.Solution.Repository.IRepository;

namespace SK.Solution.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) { 
          _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
            if(File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            if (product != null) { 
                _context.Products.Remove(product);
               return await _context.SaveChangesAsync()>0;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetProductAsync(int Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == Id);
            if (product == null)
            {
                return new Product();
            }
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productResult = await _context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (productResult is not null)
            {
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
            return product;
        }
    }
}
