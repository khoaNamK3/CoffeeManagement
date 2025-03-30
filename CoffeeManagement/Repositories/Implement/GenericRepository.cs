﻿using CoffeeManagement.MetaData;
using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace CoffeeManagement.Repositories.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        #region GetALl async 
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).Select(selector).FirstOrDefaultAsync();

            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> GetListAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? take = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            if (take.HasValue) query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public virtual async Task<ICollection<TResult>> GetListAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? take = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            IQueryable<TResult> resultQuery = query.Select(selector);

            if (take.HasValue) resultQuery = resultQuery.Take(take.Value);

            return await resultQuery.ToListAsync();
        }

        public Task<PagingResponse<T>> GetPagingListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1,
            int size = 10)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).ToPagingResponse(page, size, 1);
            return query.ToPagingResponse(page, size, 1);
        }

        public Task<PagingResponse<TResult>> GetPagingListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1, int size = 10)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).Select(selector).ToPagingResponse(page, size, 1);
            return query.Select(selector).ToPagingResponse(page, size, 1);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null) return await _dbSet.CountAsync(predicate);
            return await _dbSet.CountAsync();
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).SingleOrDefaultAsync();

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).Select(selector).SingleOrDefaultAsync();

            return await query.Select(selector).SingleOrDefaultAsync();
        }
        #endregion

        #region Insert 
        public async Task InsertAsync(T entity)
        {
            if (entity == null) return;
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual IQueryable<T> CreateBaseQuery(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool asNoTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }
        #endregion
    }
}
