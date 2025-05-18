using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.Visit.Domain.Entities;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SK.Visit.Infrastructure.PostgreSql.Configuration
{
    class VisitConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );
            builder.HasKey(entity => entity.Id);
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(255) // Optional: specify max length
               .HasAnnotation("ErrorMessage", "Name is missing..");

            builder.Property(p => p.Date)
              .IsRequired()
              .HasConversion(
                        v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified)
                    )
                   .HasColumnType("timestamp")
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

            builder.Property(p => p.CreatedDate)
                .HasConversion(dateTimeConverter);

            builder.Property(p => p.LastModifiedDate)
               .HasConversion(dateTimeConverter);


        }
    }
}
