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
        //根据id查询
        Task<T> QueryByIdAsync(int id);

        //插入
        Task<int> InsertAsync(T entity);

        //更新
        Task<bool> UpdateAsync(T entity);

        //根据id删除
        Task<bool> DeleteByIdAsync(int id);

        Task<List<T>> QueryAll();

        //根据语句动态查询
        Task<List<T>> QueryAsync(Expression<Func<T,bool>> expression);
    }
}
