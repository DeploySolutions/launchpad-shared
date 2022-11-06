using Amazon.SecretsManager;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.AWS.SecretsManager;
using DeploySoftware.LaunchPad.AWS.SecretsManager.Services;
using DeploySoftware.LaunchPad.Core.Abp.Application;
using Microsoft.Extensions.Configuration;

namespace DeploySoftware.LaunchPad.AWS.Abp.SecretsManager.Services
{
    public partial class AwsSecretsManagerService : SystemIntegrationServiceBase, IAwsSecretsManagerService
    {
        public IAwsSecretsManagerHelper Helper { get; set; }

        protected AwsSecretsManagerService() :base()
        {
        }

        
        public AwsSecretsManagerService(
            ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile,
            AmazonSecretsManagerConfig secretManagerConfig = null
        ) : base(logger, configurationRoot)
        {
            var secretHelperFactory = new AwsSecretsManagerHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile, secretManagerConfig);
        }



        public AwsSecretsManagerService(ILogger logger, IAwsSecretsManagerHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
