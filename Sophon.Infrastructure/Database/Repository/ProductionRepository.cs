using Sophon.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class ProductionRepository : RepositoryBase<Production>
    {
        public ProductionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Production>> GetProductByBarcode(string barcode)
        {
            return QueryAsync(p => p.SerialNo == barcode);
        }

        public Task<List<Production>> GetProductByLot(string partNo)
        {
            return QueryAsync(p => p.PartNo == partNo);
        }

        public Task<List<Production>> GetProductByResult(int result)
        {
            return QueryAsync(p => p.Result == result);
        }
    }
}