using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<T> QueryByIdAsync(int id);

        Task<int> InsertAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<List<T>> QueryAllAsync();

        Task<List<T>> QueryAsync(Expression<Func<T,bool>> expression);

        Task<T> QuerySingleAsync(Expression<Func<T, bool>> expression);
    }
}
