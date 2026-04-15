namespace Sophon.Core
{
    public interface IFlowContextFactory
    {
        IFlowContext CreateFlowContext(string flowName);
    }
}