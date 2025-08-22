using Microsoft.EntityFrameworkCore;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities.Product;
using SK.Inventory.Infrastructure.SqlServer.Persistence;

namespace SK.Inventory.Infrastructure.SqlServer.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly InventoryDbContext _context;
        public CategoryRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var categoryResult = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryResult is not null)
            {
                categoryResult.Name = category.Name;
                _context.Categories.Update(categoryResult);
                return categoryResult;
            }
            return category;
        }

        public async Task<(List<Category> categories, int TotalCount)> GetAllAsync(int pageIndex = 0, int pageSize = 0)
        {
            var query = _context.Categories.AsNoTracking();

            var totalCount = await query.CountAsync();
            if (pageSize != 0)
            {
                var pagedCategories = await query
               .OrderBy(q => q.Id) // Always order before skip/take
               .Skip(pageIndex * pageSize)
               .Take(pageSize)
               .ToListAsync();
                return (pagedCategories, totalCount);
            }

            return (await query.OrderBy(q => q.Id).ToListAsync(), totalCount);
        }
    }
}
