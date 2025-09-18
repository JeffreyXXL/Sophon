using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IHttpProtocol
    {
        bool IsConnected { get; }
        string IP { get; set; }
        int Port { get; set; }


        void Connect();
        void Disconnect();

        Task PostAsync();

        Task<T> GetAsync<T>();
    }
}
