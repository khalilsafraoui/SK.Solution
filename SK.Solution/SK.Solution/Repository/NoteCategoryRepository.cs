using Microsoft.EntityFrameworkCore;
using SK.Solution.Data;
using SK.Solution.Data.Entities;
using SK.Solution.Data.Entities.NoteManagment;
using SK.Solution.Repository.IRepository;

namespace SK.Solution.Repository
{
    public class NoteCategoryRepository : INoteCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<NoteCategory> CreateAsync(NoteCategory category)
        {
            await _context.NoteCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var category = await _context.NoteCategories.FirstOrDefaultAsync(c => c.Id == Id);
            if (category != null)
            {
                _context.NoteCategories.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<NoteCategory>> GetCategoriesAsync(string userId)
        {
            return await _context.NoteCategories.Where(x => x.UserId.Equals(userId)).OrderBy(u=>u.DisplayOrdre).ToListAsync();
        }

        public async Task<NoteCategory> GetCategoryAsync(int Id)
        {
            var category = await _context.NoteCategories.FirstOrDefaultAsync(c => c.Id == Id);
            if (category == null)
            {
                return new NoteCategory();
            }
            return category;
        }

        public async Task<NoteCategory> UpdateAsync(NoteCategory category)
        {
            var categoryResult = await _context.NoteCategories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryResult is not null)
            {
                categoryResult.Name = category.Name;
                categoryResult.DisplayOrdre = category.DisplayOrdre;
                _context.NoteCategories.Update(categoryResult);
                await _context.SaveChangesAsync();
                return categoryResult;
            }
            return category;
        }
    }
}
