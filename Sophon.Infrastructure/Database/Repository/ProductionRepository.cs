using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class ProductionRepository : RepositoryBase<Production>
    {
        #region 构造函数
        public ProductionRepository(DbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region 属性

        #endregion

        #region 字段

        #endregion

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
        #endregion
    }
}