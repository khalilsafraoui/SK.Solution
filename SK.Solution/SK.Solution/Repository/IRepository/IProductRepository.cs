using SK.Solution.Data.Entities;

namespace SK.Solution.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<Product> CreateAsync(Product category);
        public Task<Product> UpdateAsync(Product category);
        public Task<bool> DeleteAsync(int Id);
        public Task<Product> GetProductAsync(int Id);
        public Task<IEnumerable<Product>> GetProductsAsync();
    }
}
