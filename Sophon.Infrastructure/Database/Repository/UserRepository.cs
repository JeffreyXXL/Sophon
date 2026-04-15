using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region 构造函数

        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion 构造函数

        #region 方法

        public Task<User> GetUserByName(string name)
        {
            return QuerySingleAsync(u => u.UserName == name);
        }

        public Task<List<string>> GetAllUserNames()
        {
            return QueryAllAsync().ContinueWith(t => t.Result.Select(u => u.UserName).ToList());
        }

        public string GetPasswordByUserName(string name)
        {
            return _dbContext.Db.Queryable<User>().Where(u => u.UserName == name).Select(u => u.Password).Single();
        }

        public UserLevel GetLevelByUserName(string name)
        {
            return _dbContext.Db.Queryable<User>().Where(u => u.UserName == name).Select(u => u.UserLevel).Single();
        }

        public bool ChangePassword(string name, string newPassword)
        {
            return _dbContext.Db.Updateable<User>()
                                .SetColumns(u => u.Password == newPassword)
                                .Where(u => u.UserName == name)
                                .ExecuteCommand() > 0;
        }

        public bool DeleteUser(string name)
        {
            return _dbContext.Db.Deleteable<User>().Where(u => u.UserName == name).ExecuteCommand() > 0;
        }

        #endregion 方法
    }
}