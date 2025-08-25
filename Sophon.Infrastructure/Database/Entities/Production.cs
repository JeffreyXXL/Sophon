using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    [SugarTable("Production")]
    public class Production
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string SerialNo { get; set; }

        public string LotNo { get; set; }

        public string Result { get; set; }

        public string WorkStation { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double CycleTime { get; set; }
    }
}
