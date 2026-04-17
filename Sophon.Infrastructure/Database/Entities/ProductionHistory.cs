using SqlSugar;
using System;

namespace Sophon.Infrastructure
{
    [SugarTable("ProductionHistory")]
    public class ProductionHistory : IEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public string PartNo { get; set; }

        public int TotalCount { get; set; }

        public int OkCount { get; set; }

        public int NgCount { get; set; }

        public DateTime Date { get; set; }
    }
}