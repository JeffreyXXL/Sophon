namespace Sophon.Core
{
    public interface IFlowEngineFactory
    {
        IFlowEngine CreateFlowEngine(string flowName);
    }
}