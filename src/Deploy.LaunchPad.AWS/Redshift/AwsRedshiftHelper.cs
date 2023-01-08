using Amazon.Redshift;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.Redshift
{
    public partial class AwsRedshiftHelper : AwsHelperBase<AmazonRedshiftConfig>, IAwsRedshiftHelper
    {
        public AwsRedshiftHelper() : base()
        {
        }

        public AwsRedshiftHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
