using SK.Solution.Data.Entities.NoteManagment;

namespace SK.Solution.Repository.IRepository
{
    public interface INoteCategoryRepository
    {
        public Task<NoteCategory> CreateAsync(NoteCategory category);
        public Task<NoteCategory> UpdateAsync(NoteCategory category);
        public Task<bool> DeleteAsync(int Id);
        public Task<NoteCategory> GetCategoryAsync(int Id);
        public Task<IEnumerable<NoteCategory>> GetCategoriesAsync(string userId);
    }
}
