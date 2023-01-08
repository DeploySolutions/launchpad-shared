using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.SQS;
using Deploy.LaunchPad.AWS.SQS.Services;
using Deploy.LaunchPad.Core.Abp.Application;

namespace Deploy.LaunchPad.AWS.Abp.SQS.Services
{
    public partial class AwsSQSService : SystemIntegrationServiceBase, IAwsSQSService
    {
        public IAwsSQSHelper Helper { get; set; }

        public AwsSQSService(ILogger logger,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsSQSHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        public AwsSQSService(ILogger logger, IAwsSQSHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
