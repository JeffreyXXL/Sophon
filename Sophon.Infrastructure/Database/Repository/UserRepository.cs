using Sophon.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

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
    }
}