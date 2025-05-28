using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Infrastructure.PostgreSql.Configuration.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.OrderTotal)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Total order is missing..");

            builder.Property(entity => entity.OrderDate)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Please Select order date..");

            builder.Property(entity => entity.Status)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Status is missing..");

            builder.Property(entity => entity.Name)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Please enter name..");

            builder.Property(entity => entity.PhoneNumber)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Please enter Phone number..");

            builder.Property(entity => entity.Email)
                .IsRequired()
                .HasAnnotation("ErrorMessage", "Please enter Email..");
        }
    }
}
