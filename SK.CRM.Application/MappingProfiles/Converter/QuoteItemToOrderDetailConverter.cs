using AutoMapper;
using SK.CRM.Domain.Entities;
using SK.CRM.Domain.Entities.Quote;

namespace SK.CRM.Application.MappingProfiles.Converter
{
    public class QuoteItemToOrderDetailConverter : ITypeConverter<QuoteItem, OrderDetail>
    {
        public OrderDetail Convert(QuoteItem source, OrderDetail destination, ResolutionContext context)
        {
            return new OrderDetail
            {
                Id = source.Id,
                ProductId = source.ProductId,
                ProductName = source.ProductName,
                Quantity = source.Quantity,
                Price = source.UnitPrice ?? 0,
                DiscountRate = source.DiscountRate,
                TaxRate = source.TaxRate,
                OrderId = Guid.Empty,
            };
        }
    }

}
