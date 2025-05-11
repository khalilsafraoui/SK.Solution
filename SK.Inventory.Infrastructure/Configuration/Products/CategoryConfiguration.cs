using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.Inventory.Domain.Entities.Product;


namespace SK.Inventory.Infrastructure.SqlServer.Configuration.Products
{
    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(255) // Optional: specify max length
               .HasAnnotation("ErrorMessage", "Please enter name..");
        }
    }
}
