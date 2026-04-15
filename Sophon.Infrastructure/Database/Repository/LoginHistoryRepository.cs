namespace Sophon.Infrastructure
{
    public class LoginHistoryRepository : RepositoryBase<LoginHistory>
    {
        public LoginHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}