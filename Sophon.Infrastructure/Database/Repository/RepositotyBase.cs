using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class RepositotyBase<T> : IRepository<T> where T : class, IEntity, new()
    {
        #region 构造函数
        public RepositotyBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        protected readonly DbContext _dbContext;
        #endregion

        #region 方法
        public virtual Task<T> QueryByIdAsync(int id)
        {
            return _dbContext.Db.Queryable<T>().InSingleAsync(id);
        }

        public virtual Task<int> InsertAsync(T entity)
        {
            return _dbContext.Db.Insertable(entity).ExecuteReturnIdentityAsync();
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            return await _dbContext.Db.Updateable(entity).ExecuteCommandAsync() > 0;
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            return await _dbContext.Db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
        }

        public Task<List<T>> QueryAll()
        {
            return _dbContext.Db.Queryable<T>().ToListAsync();
        }

        public Task<List<T>> QueryAsync(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Db.Queryable<T>().Where(expression).ToListAsync();
        }

        public Task<T> QuerySingleAsync(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Db.Queryable<T>().Where(expression).SingleAsync();
        }
        #endregion
    }
}
