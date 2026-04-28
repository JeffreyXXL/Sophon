using Sophon.Common;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class ProductionHistoryRepository : RepositoryBase<ProductionHistory>
    {
        public ProductionHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}