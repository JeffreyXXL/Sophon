namespace Sophon.Infrastructure
{
    public class LoginHistoryRepository : RepositoryBase<LoginHistory>
    {
        #region 构造函数

        public LoginHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion 构造函数
    }
}