using Microsoft.EntityFrameworkCore;
using SK.Note.Domain.Entities;


namespace SK.Note.Infrastructure.PostgreSql.Persistence
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options) { }

        public DbSet<NoteCategory> NoteCategories { get; set; }

        public DbSet<Domain.Entities.Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
