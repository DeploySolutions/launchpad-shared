using Abp;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Application
{
    [Serializable()]
    public abstract class SystemIntegrationServiceBase : AbpServiceBase, ISystemIntegrationService
    {
        protected readonly IConfigurationRoot _configurationRoot;
        public IConfigurationRoot ConfigurationRoot { get { return _configurationRoot; } }


        public new ILogger Logger { get; set; }

        public SystemIntegrationServiceBase() 
        {
            Logger = NullLogger.Instance;
        }

        public SystemIntegrationServiceBase(ILogger logger)
        {
            Logger = logger;
        }

        public SystemIntegrationServiceBase(ILogger logger, IConfigurationRoot configurationRoot)
        {
            Logger = logger;
            _configurationRoot = configurationRoot;
        }

    }
}
