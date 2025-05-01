using Microsoft.EntityFrameworkCore;
using SK.Note.Application.Interfaces;
using SK.Note.Infrastructure.SqlServer.Persistence;

namespace SK.Note.Infrastructure.SqlServer.Repositories
{
    public class NoteRepository : GenericRepository<Domain.Entities.Note>, INoteRepository
    {
        private readonly NoteDbContext _context;
        
        public NoteRepository(NoteDbContext context) : base(context)
        {
            _context = context;
            
        }

        public async Task<Domain.Entities.Note> CreateAsync(Domain.Entities.Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(c => c.Id == Id);

            if (note != null)
            {
                _context.Notes.Remove(note);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Domain.Entities.Note> GetNoteAsync(int Id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(c => c.Id == Id);
            if (note == null)
            {
                return new Domain.Entities.Note();
            }
            return note;
        }

        public async Task<IEnumerable<Domain.Entities.Note>> GetNotesAsync(string userId)
        {
            return await _context.Notes.Include(x => x.NoteCategory).Where(x => x.UserId.Equals(userId)).OrderBy(u => u.DisplayOrdre).ToListAsync();
        }

        public async Task<Domain.Entities.Note> UpdateAsync(Domain.Entities.Note note)
        {
            var noteResult = await _context.Notes.FirstOrDefaultAsync(c => c.Id == note.Id);
            if (noteResult is not null)
            {
                noteResult.Title = note.Title;
                noteResult.DisplayOrdre = note.DisplayOrdre;
                noteResult.Description = note.Description;
                noteResult.DescriptionEn = note.DescriptionEn;
                noteResult.CategoryId = note.CategoryId;
                _context.Notes.Update(noteResult);
                await _context.SaveChangesAsync();
                return noteResult;
            }
            return note;
        }

    }
}
