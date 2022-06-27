using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Lambda
{
    public partial class AwsLambdaHelper : AwsHelperBase, IAwsLambdaHelper
    {
        public AwsLambdaHelper() : base()
        {
        }

        public AwsLambdaHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
