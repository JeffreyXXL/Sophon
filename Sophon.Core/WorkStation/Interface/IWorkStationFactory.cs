using System.Collections.Concurrent;

namespace Sophon.Core
{
    public interface IWorkStationFactory
    {
        ConcurrentDictionary<string, IWorkStation> WorkStationcache { get; }

        IWorkStation CreateWorkStation(string workStationName);
    }
}