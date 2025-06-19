using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities.Quote;
using SK.CRM.Infrastructure.Persistence;

namespace SK.CRM.Infrastructure.Repositories
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
                return await _context.Quotes.Where(o => o.UserId == userId).AsNoTracking().ToListAsync();
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

        public async Task<IEnumerable<Quote>> GetByStatusesAsync(IEnumerable<string> statuses, string? userId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return await _context.Quotes
                    .Where(q => statuses.Contains(q.Status))
                    .ToListAsync();
            }
            return await _context.Quotes
                .Where(q => q.UserId == userId && statuses.Contains(q.Status))
                .ToListAsync();
        }
    }
}
