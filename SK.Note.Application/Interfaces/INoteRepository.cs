namespace SK.Note.Application.Interfaces
{
    public interface INoteRepository : IGenericRepository<Domain.Entities.Note>
    {
        // You can add custom methods specific to Customer, if needed
        public Task<Domain.Entities.Note> CreateAsync(Domain.Entities.Note note);
        public Task<Domain.Entities.Note> UpdateAsync(Domain.Entities.Note note);
        public Task<bool> DeleteAsync(int Id);
        public Task<Domain.Entities.Note> GetNoteAsync(int Id);
        public Task<IEnumerable<Domain.Entities.Note>> GetNotesAsync(string userId);
    }
}
