

using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities.Quote;
using SK.CRM.Infrastructure.PostgreSql.Persistence;

namespace SK.CRM.Infrastructure.PostgreSql.Repositories
{
    public class QuoteItemRepository : GenericRepository<QuoteItem>, IQuoteItemRepository
    {
        private readonly CrmDbContext _context;
        public QuoteItemRepository(CrmDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateItemsAsync(Guid id, List<QuoteItem> items)
        {
                await DeleteRangeAsync(x => x.QuoteId.Equals(id));
                await CreateRangeAsync(items);
        }
    }
}
