using SK.Solution.Data.Entities.NoteManagment;

namespace SK.Solution.Repository.IRepository
{
    public interface INoteRepository
    {
        public Task<Note> CreateAsync(Note note);
        public Task<Note> UpdateAsync(Note note);
        public Task<bool> DeleteAsync(int Id);
        public Task<Note> GetNoteAsync(int Id);
        public Task<IEnumerable<Note>> GetNotesAsync(string userId);
    }
}
