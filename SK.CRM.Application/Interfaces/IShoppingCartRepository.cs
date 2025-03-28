﻿using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Interfaces
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        // You can add custom methods specific to ShoppingCart, if needed
        public Task<bool> UpdateCartAsync(string userId, int product, int updateBy);

        public Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId);

        public Task<bool> ClearCartAsync(string? userId);

        public Task<int> GetTotalCartCountAsync(string? userId);
    }
}
