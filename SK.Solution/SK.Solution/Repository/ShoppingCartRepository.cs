using Microsoft.EntityFrameworkCore;
using SK.Solution.Data;
using SK.Solution.Data.Entities;
using SK.Solution.Repository.IRepository;

namespace SK.Solution.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ClearCartAsync(string? userId)
        {
            var cartItems = await _context.ShoppingCarts.Where(x => x.UserId == userId).ToListAsync();
            _context.ShoppingCarts.RemoveRange(cartItems);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _context.ShoppingCarts.Where(x => x.UserId == userId).Include(u => u.Product).ToListAsync();
        }

        public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userId)) return false;
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
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
