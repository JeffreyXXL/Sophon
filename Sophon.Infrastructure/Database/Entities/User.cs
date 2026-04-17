using SqlSugar;
using System;

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
        public UserLevel UserLevel { get; set; }

        [SugarColumn(IsNullable = false)]
        public DateTime CreateTime { get; set; }

        [SugarColumn(IsNullable = false)]
        public DateTime LatestChangeTime { get; set; }
    }

    public enum UserLevel
    {
        None = 0,
        Operator = 1,
        Engineer = 2,
        Admin = 10
    }
}