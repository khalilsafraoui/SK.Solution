
using Microsoft.EntityFrameworkCore;
using SK.Inventory.Application.Exceptions;
using SK.Inventory.Application.Interfaces;
using SK.Inventory.Domain.Entities;
using SK.Inventory.Infrastructure.SqlServer.Persistence;
using System.Linq.Expressions;


namespace SK.Inventory.Infrastructure.SqlServer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>,
    IGuidEntityRepository<T>,
    IIntEntityRepository<T> where T : BaseEntity
    {
        private readonly InventoryDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(InventoryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null) throw new NotFoundException(nameof(T),entity.Id);

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
