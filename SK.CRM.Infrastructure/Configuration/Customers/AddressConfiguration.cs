using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Infrastructure.Configuration.Customers
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(a => a.Latitude)
                         .HasColumnType("decimal(9,6)");

            builder.Property(a => a.Longitude)
                        .HasColumnType("decimal(9,6)");

        }
    }
}
