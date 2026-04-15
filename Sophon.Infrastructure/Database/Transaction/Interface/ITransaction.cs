using System;
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