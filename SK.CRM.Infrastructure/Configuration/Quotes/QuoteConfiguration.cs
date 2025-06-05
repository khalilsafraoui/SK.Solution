using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities.Quote;

namespace SK.CRM.Infrastructure.Configuration.Quotes
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.HasKey(entity => entity.Id);
           

            builder.Property(entity => entity.Status)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Status is missing..");

            builder.Property(q => q.UserId)
            .IsRequired();

            builder.Property(q => q.CustomerId)
                .IsRequired();

            builder.Property(q => q.AddressId)
                .IsRequired();

            builder.Property(q => q.Currency)
                .IsRequired()
                .HasMaxLength(3) // ISO currency codes are 3 letters (e.g., EUR)
                .HasDefaultValue("EUR");

            builder.Property(q => q.GlobalDiscountRate)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(q => q.TaxRate)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(q => q.Notes)
                .HasMaxLength(2000);

            builder.HasMany(q => q.Items)
                .WithOne()
                .HasForeignKey("QuoteId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
