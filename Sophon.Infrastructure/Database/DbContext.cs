using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Common;

namespace Sophon.Infrastructure
{
    public class DbContext
    {
        public SqlSugarClient Db { get; }

        public readonly ILoggerManager _logger;

        public DbContext(string connectionstring, ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("Database");
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.Sqlite,
                ConnectionString = connectionstring,//ConfigurationManager.AppSettings["ConnectionString"],
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            //回调记录sql语句
            Db.Aop.OnLogExecuted = (sql, pars) =>
            {
                _logger.Debug(sql);
            };
        }
    }
}
