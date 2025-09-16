using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IOpcProtocol
    {
        bool IsConnected { get; }
        void Connect();
        void Disconnect();


        Task<T> ReadVariableAsync<T>(string variableName);
        Task WriteVariableAsync<T>(string variableName, T value);


        Task<Dictionary<string, object>> ReadVariablesAsync(IEnumerable<string> variableNames);
        Task WriteVariablesAsync(Dictionary<string, object> values);
    }
}
