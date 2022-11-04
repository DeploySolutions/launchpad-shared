using Amazon.SimpleNotificationService;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SNS
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
