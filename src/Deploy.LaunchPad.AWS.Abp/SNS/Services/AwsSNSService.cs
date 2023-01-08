using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.SNS;
using Deploy.LaunchPad.AWS.SNS.Services;
using Deploy.LaunchPad.Core.Abp.Application;
using Microsoft.Extensions.Configuration;

namespace Deploy.LaunchPad.AWS.Abp.SNS.Services
{
    public partial class AwsSNSService : SystemIntegrationServiceBase, IAwsSNSService
    {
        public IAwsSNSHelper Helper { get; set; }

        protected AwsSNSService() : base()
        {
        }

        public AwsSNSService(ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsSNSHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        public AwsSNSService(ILogger logger, IAwsSNSHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
