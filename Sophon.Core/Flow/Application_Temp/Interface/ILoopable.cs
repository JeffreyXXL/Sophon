using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 该步骤可以作为循环流程最终步
    /// 实现该接口的方法构造函数里面应该添加这两个属性的注入，默认都为0
    /// 可参考FlowStep_Delay
    /// </summary>
    public interface ILoopable
    {
        /// <summary>
        /// 循环总次数
        /// </summary>
        int TotalLoops { get; }
        /// <summary>
        /// 循环开始步
        /// </summary>
        int LoopStartStepIndex { get; }
    }
}
