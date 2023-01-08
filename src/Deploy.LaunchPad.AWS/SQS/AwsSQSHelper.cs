using Amazon.SQS;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.SQS
{
    public partial class AwsSQSHelper : AwsHelperBase<AmazonSQSConfig>, IAwsSQSHelper
    {
        public AwsSQSHelper() : base()
        {
        }

        public AwsSQSHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
