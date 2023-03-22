using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using System;

namespace Deploy.LaunchPad.Core.Application
{
    [Serializable()]
    public abstract class SystemIntegrationServiceBase : ILaunchPadSystemIntegrationService
    {
        protected readonly IConfigurationRoot _configurationRoot;
        public IConfigurationRoot ConfigurationRoot { get { return _configurationRoot; } }


        public ILogger Logger { get; set; }

        protected SystemIntegrationServiceBase()
        {
            Logger = NullLogger.Instance;
        }

        protected SystemIntegrationServiceBase(ILogger logger)
        {
            Logger = logger;
        }

        protected SystemIntegrationServiceBase(ILogger logger, IConfigurationRoot configurationRoot)
        {
            Logger = logger;
            _configurationRoot = configurationRoot;
        }

    }
}
