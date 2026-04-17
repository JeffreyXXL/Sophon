namespace Sophon.Infrastructure
{
    public class ProductionHistoryRepository : RepositoryBase<ProductionHistory>
    {
        public ProductionHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}