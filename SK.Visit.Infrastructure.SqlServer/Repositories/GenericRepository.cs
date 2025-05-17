
using Microsoft.EntityFrameworkCore;
using SK.Visit.Application.Exceptions;
using SK.Visit.Application.Interfaces;
using SK.Visit.Domain.Entities;
using SK.Visit.Infrastructure.SqlServer.Persistence;
using System.Linq.Expressions;


namespace SK.Visit.Infrastructure.SqlServer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>,
    IGuidEntityRepository<T>,
    IIntEntityRepository<T> where T : BaseEntity
    {
        private readonly VisitDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(VisitDbContext context)
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
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity == null) throw new NotFoundException(nameof(T),entity.Id);

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return existingEntity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }
      
    }

}
