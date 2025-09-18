using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class HttpProtocol : IHttpProtocol
    {
        public bool IsConnected => throw new NotImplementedException();

        public string IP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Port { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task PostAsync()
        {
            throw new NotImplementedException();
        }
    }
}
