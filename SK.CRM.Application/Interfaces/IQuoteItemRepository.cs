using SK.CRM.Domain.Entities.Quote;

namespace SK.CRM.Application.Interfaces
{
    public interface IQuoteItemRepository : IGenericRepository<QuoteItem>
    {
        // You can add custom methods specific to Order, if needed
        Task UpdateItemsAsync(Guid id,List<QuoteItem> items);
    }
}
