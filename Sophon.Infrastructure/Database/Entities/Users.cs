using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    [SugarTable("Users")]
    public class Users
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string UserName { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Password { get; set; }

        [SugarColumn(IsNullable = false)]
        public int UserLevel { get; set; }
    }
}
