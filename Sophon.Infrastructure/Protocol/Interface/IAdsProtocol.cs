using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IAdsProtocol
    {
        bool IsConnected { get; }
        string TargetNetId { get; set; }
        int TargetPort { get; set; }
        int LocalPort { get; set; }
        int Timeout { get; set; }

        void Connect();

        void Disconnect();

        Task<T> ReadVariableAsync<T>(string variableName);

        Task WriteVariableAsync<T>(string variableName, T value);

        Task<Dictionary<string, object>> ReadVariablesAsync(IEnumerable<string> variableNames);

        Task WriteVariablesAsync(Dictionary<string, object> values);
    }
}