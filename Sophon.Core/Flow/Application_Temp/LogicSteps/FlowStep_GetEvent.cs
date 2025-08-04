using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 模块间通信事件获取
    /// </summary>
    public class FlowStep_GetEvent : FlowStepBase
    {
        #region 构造函数
        public FlowStep_GetEvent(string stepName) : base(stepName)
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 字段

        #endregion

        #region 方法
        protected override async Task ExecuteCoreAsync(IFlowContext context, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
