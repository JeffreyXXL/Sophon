using Sophon.Common;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class MotionProvider : IMotionProvider
    {
        public IAxisController Axis { get; }
        public IIoController Io { get; }

        public MotionProvider(IAxisController axis, IIoController io)
        {
            Axis = axis;
            Io = io;
        }
    }
}