using Abp;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Configuration
{
    [Serializable()]
    public abstract class SystemIntegrationServiceBase : AbpServiceBase, ISystemIntegrationService
    {
        public new ILogger Logger { get; set; }

        protected SystemIntegrationServiceBase() { }

        public SystemIntegrationServiceBase(ILogger logger)
        {
            Logger = logger;
        }

    }
}
