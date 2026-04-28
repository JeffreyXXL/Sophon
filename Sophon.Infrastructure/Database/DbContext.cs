using Common;
using Sophon.Common;
using SqlSugar;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Delegate)]
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