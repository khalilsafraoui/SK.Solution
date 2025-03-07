using Microsoft.EntityFrameworkCore;
using SK.Solution.Data;
using SK.Solution.Data.Entities;
using SK.Solution.Repository.IRepository;

namespace SK.Solution.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) { 
          _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if (category != null) { 
                _context.Categories.Remove(category);
               return await _context.SaveChangesAsync()>0;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if (category == null)
            {
                return new Category();
            }
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var categoryResult = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryResult is not null)
            {
                categoryResult.Name = category.Name;
                _context.Categories.Update(categoryResult);
                await _context.SaveChangesAsync();
                return categoryResult;
            }
            return category;
        }
    }
}
