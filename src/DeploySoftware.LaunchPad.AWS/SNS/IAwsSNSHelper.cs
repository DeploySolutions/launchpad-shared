using Amazon.SimpleNotificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SNS
{
    public interface IAwsSNSHelper : IAwsHelper<AmazonSimpleNotificationServiceConfig>
    {
    }
}
