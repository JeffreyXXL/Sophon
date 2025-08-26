using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class Transaction : ITransaction
    {
        #region 构造函数
        public Transaction(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly DbContext _dbContext;
        private bool _disposed;
        #endregion

        #region 方法

        /// <summary>
        /// 事务执行
        /// </summary>
        /// <param name="action">传入多条同时执行的语句</param>
        /// <returns></returns>
        public async Task ExecuteTranAsync(Func<Task> func)
        {
            try
            {
                BeginTran();
                await func();
                CommitTran();
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }
        }

        public void BeginTran()
        {
            _dbContext.Db.Ado.BeginTran();
        }

        public void CommitTran()
        {
            _dbContext.Db.Ado.CommitTran();
        }

        public void RollBack()
        {
            _dbContext.Db.Ado.RollbackTran();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                RollBack();
                _disposed = true;
            }
        }

        #endregion
    }
}
