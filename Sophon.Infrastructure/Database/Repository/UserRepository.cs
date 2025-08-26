using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class UserRepository : RepositotyBase<User>
    {
        #region 构造函数
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region 属性

        #endregion

        #region 字段

        #endregion

        #region 方法
        public Task<User> GetUserByName(string name)
        {
            return QuerySingleAsync(u => u.UserName == name);
        }

        #endregion
    }
}
