using SqlSugar;
using System;

namespace Sophon.Infrastructure
{
    [SugarTable("Production")]
    public class Production : IEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public string PartNo { get; set; }

        [SugarColumn(IsNullable = false)]
        public string SerialNo { get; set; }

        public string WorkStation { get; set; }

        public int Result { get; set; }

        public string ResultMsg { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double CycleTime { get; set; }
    }
}