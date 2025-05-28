using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Infrastructure.PostgreSql.Configuration.Shopping
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(entity => entity.Id);
        }
    }
}
