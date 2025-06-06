﻿using SK.Visit.Domain.Entities;
using System.Linq.Expressions;


namespace SK.Visit.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}
