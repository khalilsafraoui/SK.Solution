using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Infrastructure.Configuration.Orders
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Quantity)
                .IsRequired();
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_OrderDetails_Quantity_Positive", "[Quantity] > 0");
            });

            builder.Property(entity => entity.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(entity => entity.ProductName)
                .IsRequired();
        }
    }
}
