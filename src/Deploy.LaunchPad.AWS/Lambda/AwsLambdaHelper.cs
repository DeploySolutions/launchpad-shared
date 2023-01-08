using Amazon.Lambda;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.Lambda
{
    public partial class AwsLambdaHelper : AwsHelperBase<AmazonLambdaConfig>, IAwsLambdaHelper
    {
        public AwsLambdaHelper() : base()
        {
        }

        public AwsLambdaHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
