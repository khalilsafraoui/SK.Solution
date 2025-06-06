using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities.Quote;

namespace SK.CRM.Infrastructure.PostgreSql.Configuration.Quotes
{
    public class QuoteItemConfiguration : IEntityTypeConfiguration<QuoteItem>
    {
        public void Configure(EntityTypeBuilder<QuoteItem> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Quantity)
                .IsRequired();
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_QuoteItems_Quantity_Positive", "\"Quantity\" > 0");
            });

            builder.Property(entity => entity.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(entity => entity.ProductName)
                .IsRequired();

            builder.Property(qi => qi.DiscountRate)
           .IsRequired()
           .HasDefaultValue(0);

            builder.Property(qi => qi.ProductId)
            .IsRequired();

            builder.Property(qi => qi.QuoteId)
                .IsRequired();

            builder.Property(p => p.TaxRate)
                  .HasDefaultValue(0);
        }
    }
}
