using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface ITransaction : IDisposable
    {
        Task ExecuteTranAsync(Func<Task> func);
        void BeginTran();
        void CommitTran();
        void RollBack();
    }
}
