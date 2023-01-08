using Amazon.SimpleNotificationService;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.SNS
{
    public partial class AwsSNSHelper : AwsHelperBase<AmazonSimpleNotificationServiceConfig>, IAwsSNSHelper
    {
        public AwsSNSHelper() : base()
        {
        }

        public AwsSNSHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
