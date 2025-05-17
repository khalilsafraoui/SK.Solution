using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.Visit.Domain.Entities;

namespace SK.Visit.Infrastructure.SqlServer.Configuration
{
    class VisitConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(255) // Optional: specify max length
               .HasAnnotation("ErrorMessage", "Name is missing..");

            builder.Property(p => p.Date)
              .IsRequired()
              .HasAnnotation("ErrorMessage", "Date is missing..");

            builder.Property(p => p.IsDelevery)
                  .HasDefaultValue(true);

            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(500) // Optional
                   .HasAnnotation("ErrorMessage", "Address is missing..");

            builder.Property(p => p.CustomerId)
                  .IsRequired()
                  .HasMaxLength(500) // Optional
                  .HasAnnotation("ErrorMessage", "CustomerId is missing..");

            builder.Property(p => p.AgentId)
                 .IsRequired()
                 .HasMaxLength(500) // Optional
                 .HasAnnotation("ErrorMessage", "AgentId is missing..");

            builder.Property(p => p.AddressId)
                 .IsRequired()
                 .HasMaxLength(500) // Optional
                 .HasAnnotation("ErrorMessage", "AddressId is missing..");

        }
    }
}
