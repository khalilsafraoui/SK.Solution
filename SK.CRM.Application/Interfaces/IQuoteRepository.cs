using SK.CRM.Domain.Entities.Quote;

namespace SK.CRM.Application.Interfaces
{
    public interface IQuoteRepository : IGenericRepository<Quote>
    {
        // You can add custom methods specific to Order, if needed
        public Task<Quote> GetByIdAsync(Guid orderId);
        public Task<IEnumerable<Quote>> GetAllAsync(string? userId = null);
        
        public Task<Quote> UpdateStatusAsync(Guid orderId, string status);

        public Task<IEnumerable<Quote>> GetQuoteByStatusAsync(string status);

        Task<IEnumerable<Quote>> GetByStatusesAsync(IEnumerable<string> statuses, string? userId = null);
    }
}
