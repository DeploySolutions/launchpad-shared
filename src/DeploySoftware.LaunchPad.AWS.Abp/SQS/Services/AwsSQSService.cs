using Castle.Core.Logging;
using DeploySoftware.LaunchPad.AWS.SQS;
using DeploySoftware.LaunchPad.AWS.SQS.Services;
using DeploySoftware.LaunchPad.Core.Abp.Application;
using DeploySoftware.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Abp.SQS.Services
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
