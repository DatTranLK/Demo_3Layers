using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithInclude(Expression<Func<T, object>> includes);
        Task<IEnumerable<T>> GetAllWithOrderByDescending(Expression<Func<T, int>> orderBy);
        Task<IEnumerable<T>> GetAllWithCondition(Expression<Func<T, bool>> expression = null, List<Expression<Func<T, object>>> includes = null, Expression<Func<T, int>> orderBy = null, bool disableTracking = true);
        Task<IEnumerable<T>> GetAllWithPagination(Expression<Func<T, bool>> expression = null, List<Expression<Func<T, object>>> includes = null, Expression<Func<T, int>> orderBy = null, bool disableTracking = true, int? page = null, int? pageSize = null);

        Task<int> CountAll(Expression<Func<T, bool>> expression = null);
        Task<T> GetById(object id);
        Task<T> GetByIdWithCondition(Expression<Func<T, bool>> expression = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression);
        Task Insert(T obj);
        Task Delete(T obj);
        Task Update(T obj);
        Task Save();

    }
}
