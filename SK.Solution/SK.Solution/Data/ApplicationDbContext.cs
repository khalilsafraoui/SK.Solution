using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SK.Solution.Data.Entities;

namespace SK.Solution.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                new Category { Id= 1, Name="Breakfast"},
                new Category { Id = 2, Name = "Lunch" },
                new Category { Id = 3, Name = "Dinner" }
                );
        }
    }
}
