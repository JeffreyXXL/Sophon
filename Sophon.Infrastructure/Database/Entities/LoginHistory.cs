using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    [SugarTable("LoginHistory")]
    public class LoginHistory : IEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public string UserName { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime LogoutTime { get; set; }
    }
}
