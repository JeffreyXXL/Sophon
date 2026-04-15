using SqlSugar;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Sophon.Infrastructure
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        #region 构造函数

        public DatabaseInitializer(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion 构造函数

        #region 字段

        private readonly DbContext _dbContext;
        private Type[] _entityTypes;

        #endregion 字段

        #region 方法

        public void Initialize()
        {
            GetAllEntities();
            InitDbContext();
            SetDefaultData();
        }

        public void InitDbContext()
        {
            var connstr = ConfigurationManager.AppSettings["ConnectionString"];
            string fullPath = connstr.Split('=')[1].Split(';')[0].Trim();

            string directoryPath = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            _dbContext.Db.CodeFirst.InitTables(_entityTypes);
            _dbContext._logger.Debug($"成功创建{_entityTypes.Length}张表。");
        }

        public Type[] GetAllEntities()
        {
            if (_entityTypes != null)
            {
                return _entityTypes;
            }
            var assembly = typeof(User).Assembly;
            _entityTypes = assembly.GetTypes().Where(x => typeof(IEntity).IsAssignableFrom(x) &&
                                        !x.IsAbstract && !x.IsInterface &&
                                        x.IsDefined(typeof(SugarTable), false)).ToArray();

            string names = string.Join(",", _entityTypes.Select(t => t.Name));
            _dbContext._logger.Debug($"成功获取{_entityTypes.Length}个实体类，分别为{names}。");
            return _entityTypes;
        }

        private void SetDefaultData()
        {
            bool hasAnyUser = _dbContext.Db.Queryable<User>().Any();

            if (!hasAnyUser)
            {
                var adminUser = new User
                {
                    UserName = "管理员",
                    Password = "123",
                    UserLevel = UserLevel.Admin,
                    CreateTime = DateTime.Now,
                    LatestChangeTime = DateTime.Now,
                };

                _dbContext.Db.Insertable(adminUser).ExecuteCommand();
            }
        }

        #endregion 方法
    }
}