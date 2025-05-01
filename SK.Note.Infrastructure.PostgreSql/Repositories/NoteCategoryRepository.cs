using Microsoft.EntityFrameworkCore;
using SK.Note.Application.Interfaces;
using SK.Note.Domain.Entities;
using SK.Note.Infrastructure.PostgreSql.Persistence;

namespace SK.Note.Infrastructure.PostgreSql.Repositories
{
    public class NoteCategoryRepository : GenericRepository<NoteCategory>, INoteCategoryRepository
    {
        private readonly NoteDbContext _context;
        
        public NoteCategoryRepository(NoteDbContext context) : base(context)
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
            return await _context.NoteCategories.Where(x => x.UserId.Equals(userId)).OrderBy(u => u.DisplayOrdre).ToListAsync();
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
