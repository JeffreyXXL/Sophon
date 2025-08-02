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
    /// 并行运行
    /// </summary>
    public class FlowStep_Parallel : FlowStepBase
    {
        #region 构造函数
        public FlowStep_Parallel(string stepName, List<List<IFlowStep>> branches, bool waitAll = true)
            : base(stepName)
        {
            _parallelBranches = branches ?? throw new ArgumentNullException(nameof(branches));
            _waitAll = waitAll;
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly List<List<IFlowStep>> _parallelBranches;
        private readonly bool _waitAll;
        #endregion

        #region 方法
        protected override Task ExecuteCoreAsync(IFlowContext context, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
