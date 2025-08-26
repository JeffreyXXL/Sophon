using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    [SugarTable("User")]
    public class User : IEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, UniqueGroupNameList = new[] { "UserNameList" })]
        public string UserName { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Password { get; set; }

        [SugarColumn(IsNullable = false)]
        public int UserLevel { get; set; }

        [SugarColumn(IsNullable = false)]
        public DateTime CreateTime { get; set; }

        [SugarColumn(IsNullable = false)]
        public DateTime LatestChangeTime { get; set; }
    }
}
