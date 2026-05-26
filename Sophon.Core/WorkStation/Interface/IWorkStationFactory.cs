using System.Collections.Concurrent;

namespace Sophon.Core
{
    public interface IWorkStationFactory
    {
        ConcurrentDictionary<string, IWorkStation> WorkStationCache { get; }

        IWorkStation CreateWorkStation(string workStationName);
    }
}