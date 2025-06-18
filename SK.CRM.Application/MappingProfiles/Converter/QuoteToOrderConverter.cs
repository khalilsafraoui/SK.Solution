using AutoMapper;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Domain.Entities;
using SK.CRM.Domain.Entities.Quote;

namespace SK.CRM.Application.MappingProfiles.Converter
{
    public class QuoteToOrderConverter : ITypeConverter<Quote, Order>
    {
        public Order Convert(Quote source, Order destination, ResolutionContext context)
        {
            if (source == null)
            {
                return null; // Handle null source gracefully
            }
            if(destination == null)
            {
                destination = new Order();
            }



            destination.Id = source.Id;
            destination.UserId = source.UserId;
            destination.CustomerId = source.CustomerId;
            destination.AddressId = source.AddressId;
            destination.Status = source.Status;
            destination.FullAddress = source.FullAddress;
            destination.OrderDate = source.CreatedDate;
            
            destination.Name = source.FullName;
            // Optional/default values
            destination.SessionId = null;
            destination.PaymentIntentId = null;

            // Map items to order details using context
            destination.OrderDetails = source.Items?
                    .Select(item => context.Mapper.Map<OrderDetail>(item))
                    .ToList() ?? new List<OrderDetail>();

            return destination;
        }
    }

}
