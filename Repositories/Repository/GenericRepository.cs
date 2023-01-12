using Enities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FstoreDBContext _dbContext;

        public GenericRepository(FstoreDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task Delete(T obj)
        {
            _dbContext.Set<T>().Remove(obj);
            await _dbContext.SaveChangesAsync();
        }

        public  async Task<IEnumerable<T>> GetAll()
        {
            var lst = await _dbContext.Set<T>().ToListAsync();
            if (lst.Count <= 0)
            {
                return null;
            }
            return lst;
        }

        public async Task<IEnumerable<T>> GetAllWithCondition(Expression<Func<T, bool>> expression = null, List<Expression<Func<T, object>>> includes = null, Expression<Func<T, int>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (expression != null) query = query.Where(expression);
            if (orderBy != null)
                return await query.OrderByDescending(orderBy).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithInclude(Expression<Func<T, object>> includes)
        {
            var lst = await _dbContext.Set<T>().Include(includes).ToListAsync();
            if (lst.Count <= 0)
            {
                return null;
            }
            return lst;
        }

        public async Task<IEnumerable<T>> GetAllWithOrderByDescending(Expression<Func<T, int>> orderBy)
        {
            var lst = await _dbContext.Set<T>().OrderByDescending(orderBy).ToListAsync();
            if (lst.Count <= 0)
            {
                return null;
            }
            return lst;
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            var lst = await _dbContext.Set<T>().Where(expression).ToListAsync();
            if (lst.Count <= 0)
            {
                return null;
            }
            return lst;
        }

        public async Task<T> GetById(object id)
        {
            var rs = await _dbContext.Set<T>().FindAsync(id);
            if (rs == null)
            {
                return null;
            }
            return rs;
        }

        public async Task<T> GetByIdWithCondition(Expression<Func<T, bool>> expression = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (expression != null) query = query.Where(expression);
            return await query.FirstOrDefaultAsync();
        }

        public async Task Insert(T obj)
        {
            _dbContext.Set<T>().Add(obj);
            await _dbContext.SaveChangesAsync(); 
        }

        public async Task Update(T obj)
        {
            _dbContext.ChangeTracker.Clear();
            _dbContext.Set<T>().Update(obj);
            await _dbContext.SaveChangesAsync();
        }
    }
}
