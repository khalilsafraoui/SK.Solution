using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SK.Solution.Data;
using SK.Solution.Data.Entities.NoteManagment;
using SK.Solution.Repository.IRepository;

namespace SK.Solution.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Note> CreateAsync(Note note)
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

        public async Task<Note> GetNoteAsync(int Id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(c => c.Id == Id);
            if (note == null)
            {
                return new Note();
            }
            return note;
        }

        public async Task<IEnumerable<Note>> GetNotesAsync(string userId)
        {
            return await _context.Notes.Include(x => x.NoteCategory).Where( x =>x.UserId.Equals(userId)).OrderBy(u => u.DisplayOrdre).ToListAsync();
        }

        public async Task<Note> UpdateAsync(Note note)
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
