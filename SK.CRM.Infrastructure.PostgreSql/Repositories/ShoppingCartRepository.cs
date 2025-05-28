using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Infrastructure.PostgreSql.Persistence;


namespace SK.CRM.Infrastructure.PostgreSql.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly CrmDbContext _context;
        public ShoppingCartRepository(CrmDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task ClearCartAsync(string? userId)
        {
            var cartItems = await _context.ShoppingCarts.Where(x => x.UserId == userId).ToListAsync();
            _context.ShoppingCarts.RemoveRange(cartItems);
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _context.ShoppingCarts.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<int> GetTotalCartCountAsync(string? userId)
        {
            int cartCount = 0;
            var cartItems = await _context.ShoppingCarts.Where(x => x.UserId == userId).ToListAsync();
            if (cartItems != null)
            {
                cartCount = cartItems.Sum(x => x.Count);
            }
            return cartCount;
        }

        public async Task UpdateCartAsync(string userId, int productId, int updateBy)
        {
            var cartItem = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
            if (cartItem == null)
            {
                var cart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };
                await _context.ShoppingCarts.AddAsync(cart);
            }
            else
            {
                cartItem.Count += updateBy;
                if (cartItem.Count < 1)
                {
                    _context.ShoppingCarts.Remove(cartItem);
                }
            }
        }
    }
}
