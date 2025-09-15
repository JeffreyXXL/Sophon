using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    /// <summary>
    /// PLC协议接口
    /// </summary>
    public interface IPlcProtocol : IProtocolBase
    {
        //读写单个变量
        Task<T> ReadVariableAsync<T>(string variableName);
        Task WriteVariableAsync<T>(string variableName, T value);


        //批量读写
        Task<Dictionary<string, object>> ReadVariablesAsync(IEnumerable<string> variableNames);
        Task WriteVariablesAsync(Dictionary<string, object> values);
    }
}
