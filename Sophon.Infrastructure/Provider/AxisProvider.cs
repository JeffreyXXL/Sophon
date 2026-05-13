using Sophon.Common;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class AxisProvider : IAxisProvider
    {
        public IAxisController Axis { get; }
        public IIoController Io { get; }

        public AxisProvider(IAxisController axis, IIoController io)
        {
            Axis = axis;
            Io = io;
        }
    }
}