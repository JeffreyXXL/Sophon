using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class FlowContext : IFlowContext
    {
        #region 构造函数

        #endregion

        #region 属性
        public int NextStepIndex { get; set; }
        public int TotalSteps { get; set; }
        public Dictionary<string, object> Data => _data;

        #endregion

        #region 字段
        private readonly Dictionary<string, object> _data;
        #endregion

        #region 方法
        public T GetData<T>(string key)
        {
            return _data.TryGetValue(key, out var value) ? (T)value : default;
        }

        public void SetData<T>(string key, T value)
        {
            _data[key] = value;
        }
        #endregion
    }
}
