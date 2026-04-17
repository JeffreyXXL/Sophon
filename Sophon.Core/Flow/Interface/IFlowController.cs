namespace Sophon.Core
{
    public interface IFlowController
    {
        /// <summary>
        /// 流程已暂停
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// 流程已停止
        /// </summary>
        bool IsStopped { get; }

        /// <summary>
        /// 流程运行中
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 当前步数
        /// </summary>
        int CurrentIndex { get; }

        void Pause();

        void Resume();

        void Stop();
    }
}