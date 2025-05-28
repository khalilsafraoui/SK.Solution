using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.Inventory.Domain.Entities.Product;

namespace SK.Inventory.Infrastructure.SqlServer.Configuration.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(255) // Optional: specify max length
               .HasAnnotation("ErrorMessage", "Please enter name..");

            builder.Property(p => p.Price)
                  .HasPrecision(18, 2)
                  .IsRequired();

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Product_Price", "[Price] >= 0.01 AND [Price] <= 1000");
            });
            builder.Property(p => p.SpecialTag)
                  .HasMaxLength(150); // Optional

            builder.Property(p => p.ImageUrl)
                  .HasMaxLength(500); // Optional

            builder.HasOne(p => p.Category)
                  .WithMany()
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
