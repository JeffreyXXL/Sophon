namespace Sophon.Infrastructure
{
    public class ProductionHistoryRepository : RepositoryBase<ProductionHistory>
    {
        #region 构造函数

        public ProductionHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion 构造函数
    }
}