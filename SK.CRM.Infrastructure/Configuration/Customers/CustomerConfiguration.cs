using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Infrastructure.Configuration.Customers
{
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(entity => entity.Id);
        }
    }
}
