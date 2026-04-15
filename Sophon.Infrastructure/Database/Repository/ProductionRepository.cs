using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class ProductionRepository : RepositoryBase<Production>
    {
        #region 构造函数

        public ProductionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion 构造函数

        #region 方法

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

        #endregion 方法
    }
}