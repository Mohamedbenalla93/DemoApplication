using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DemoAppliaction.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Infrastructure.Persistance.Context;

namespace DemoApplication.Infrastructure.Persistance.Repositories.GenericRepository
{
    public class GenericRepositoryAsync<T> : IGenericRepositotyAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<bool> AllWithoutGFAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().IgnoreQueryFilters().AllAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes)
        {
            var query = _dbContext
                .Set<T>()
                .Where(predicate)
                .AsNoTracking();

            if (includes?.Length > 0)
            {
                query = includes.Aggregate(query,
                    (current, include) => include(current));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
        }
        public async Task<bool> AnyAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync();
        }

        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes)
        {
            var query = _dbContext
                .Set<T>()
                .AsNoTracking();

            if (includes?.Length > 0)
            {
                query = includes.Aggregate(query,
                    (current, include) => include(current));
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query
                .LastOrDefaultAsync(predicate);
        }
        public async Task<T> LastOrDefaultAsync()
        {
            return await _dbContext
                .Set<T>()
                .AsNoTracking()
                .LastOrDefaultAsync();
        }

        public async Task<bool> AnyWithoutGFAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().IgnoreQueryFilters().AnyAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes)
        {
            var query = _dbContext
                .Set<T>()
                .AsNoTracking();

            if (includes?.Length > 0)
            {
                query = includes.Aggregate(query,
                    (current, include) => include(current));
            }

            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync()
        {
            return await _dbContext
                .Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<T> FirstOrDefaultWithoutQFAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes)
        {
            var query = _dbContext
                .Set<T>()
                .AsNoTracking();

            if (includes?.Length > 0)
            {
                query = includes.Aggregate(query,
                    (current, include) => include(current));
            }

            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query
                .IgnoreQueryFilters().FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is not null)
                return await _dbContext.Set<T>().CountAsync(predicate);
            else
                return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AllAsync(predicate);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (T t in entities)
            {
                _dbContext.Entry(t).State = EntityState.Modified;
            }
        }

        public void DetachEntity(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
