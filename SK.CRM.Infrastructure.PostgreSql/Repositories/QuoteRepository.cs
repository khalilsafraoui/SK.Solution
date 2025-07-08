using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities.Quote;
using SK.CRM.Infrastructure.PostgreSql.Persistence;


namespace SK.CRM.Infrastructure.PostgreSql.Repositories
{
    public class QuoteRepository : GenericRepository<Quote>, IQuoteRepository
    {
        private readonly CrmDbContext _context;
        public QuoteRepository(CrmDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quote>> GetAllAsync(string? userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _context.Quotes.Where(o => o.UserId == userId).ToListAsync();
            }
            return await _context.Quotes.ToListAsync();
        }

        public async Task<Quote> GetByIdAsync(Guid orderId)
        {
            return await _context.Quotes.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Quote>> GetQuoteByStatusAsync(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                return await _context.Quotes.Where(o => o.Status == status).ToListAsync();
            }
            return await _context.Quotes.ToListAsync();
        }

        public async Task<Quote> UpdateStatusAsync(Guid orderId, string status)
        {
            var order = await _context.Quotes.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
            }
            return order;
        }

        public async Task<(IEnumerable<Quote> Quotes, int TotalCount)> GetByStatusesAsync(
            IEnumerable<string> statuses,
            int pageIndex,
            int pageSize,
            string? userId = null)
        {
            var query = _context.Quotes.AsNoTracking()
                .Where(q => statuses.Contains(q.Status));

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(q => q.UserId == userId);
            }

            var totalCount = await query.CountAsync();

            var pagedQuotes = await query
                .OrderBy(q => q.Id) // Always order before skip/take
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (pagedQuotes, totalCount);
        }
    }
}
