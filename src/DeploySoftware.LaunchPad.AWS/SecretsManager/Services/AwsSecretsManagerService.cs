using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager.Services
{
    public partial class AwsSecretsManagerService : SystemIntegrationServiceBase, IAwsSecretsManagerService
    {
        public IAwsSecretsManagerHelper Helper { get; set; }

        protected AwsSecretsManagerService() :base()
        {
        }

        public AwsSecretsManagerService(ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) :base(logger)
        {
            var secretHelperFactory = new AwsSecretsManagerHelperFactory(logger, configurationRoot, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, configurationRoot, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        public AwsSecretsManagerService(ILogger logger, IAwsSecretsManagerHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
