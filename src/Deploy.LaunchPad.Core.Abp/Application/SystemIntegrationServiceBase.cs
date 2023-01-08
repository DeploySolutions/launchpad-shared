using Abp;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;

namespace Deploy.LaunchPad.Core.Abp.Application
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
