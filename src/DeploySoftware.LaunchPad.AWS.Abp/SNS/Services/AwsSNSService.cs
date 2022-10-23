﻿using Castle.Core.Logging;
using DeploySoftware.LaunchPad.AWS.SNS;
using DeploySoftware.LaunchPad.AWS.SNS.Services;
using DeploySoftware.LaunchPad.Core.Abp.Application;
using DeploySoftware.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Abp.SNS.Services
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