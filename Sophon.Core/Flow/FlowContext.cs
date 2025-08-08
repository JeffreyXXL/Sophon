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
        public FlowContext(string flowName, ILoggerFactory loggerFactory)
        {
            FlowName = flowName;
            _loggerFactory = loggerFactory;
            Logger = _loggerFactory.CreateLogger(flowName);
            _data = new Dictionary<string, object>();
        }
        #endregion

        #region 属性
        public string FlowName { get; }
        public int NextStepIndex { get; set; }
        public int TotalSteps { get; set; }
        public Dictionary<string, object> Data => _data;
        public ILoggerManager Logger { get; }
        #endregion

        #region 字段
        private readonly Dictionary<string, object> _data;
        private readonly ILoggerFactory _loggerFactory;
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

        /// <summary>
        /// data为浅拷贝，数据公用，其余字段不共用
        /// </summary>
        /// <returns></returns>
        public IFlowContext Clone()
        {
            var cloned = new FlowContext(this.FlowName, _loggerFactory);
            var dataField = typeof(FlowContext).GetField("_data", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            dataField?.SetValue(cloned, this._data);
            return cloned;
        }
        #endregion
    }
}
