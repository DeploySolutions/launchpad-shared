using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Methods
{
    public partial interface ILaunchPadMethodInput
    {
        public Guid? CorrelationId { get; init; }

        public ILogger Logger { get; init; }
    }
}
