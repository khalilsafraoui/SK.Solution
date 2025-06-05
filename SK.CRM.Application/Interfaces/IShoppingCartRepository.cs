using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Interfaces
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        // You can add custom methods specific to ShoppingCart, if needed
        public Task UpdateCartAsync(string userId, int product, int updateBy);
        public Task UpdateCartWithNewQuantityAsync(string userId, int product, int newQuantiy);

        public Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId);

        public Task ClearCartAsync(string? userId);

        public Task<int> GetTotalCartCountAsync(string? userId);
    }
}
