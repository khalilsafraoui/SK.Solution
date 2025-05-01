using SK.Note.Domain.Entities;

namespace SK.Note.Application.Interfaces
{
    public interface INoteCategoryRepository : IGenericRepository<NoteCategory>
    {
        public Task<NoteCategory> CreateAsync(NoteCategory category);
        public Task<NoteCategory> UpdateAsync(NoteCategory category);
        public Task<bool> DeleteAsync(int Id);
        public Task<NoteCategory> GetCategoryAsync(int Id);
        public Task<IEnumerable<NoteCategory>> GetCategoriesAsync(string userId);
    }
}
