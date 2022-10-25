using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DemoAppliaction.Core.Application.Interfaces
{
    public interface IGenericRepositotyAsync<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync();
        Task<bool> AnyWithoutGFAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AllWithoutGFAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        void DetachEntity(T entity);
        Task<T> FirstOrDefaultAsync();
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy = null, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        Task<T> LastOrDefaultAsync();

        Task<T> FirstOrDefaultWithoutQFAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderBy = null,
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
    }
}
