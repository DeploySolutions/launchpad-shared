﻿using Castle.Core.Logging;
using DeploySoftware.LaunchPad.AWS.ApiGateway.Services;
using DeploySoftware.LaunchPad.Core.Abp.Application;
using Microsoft.Extensions.Configuration;
using System;

namespace DeploySoftware.LaunchPad.AWS.Abp.ApiGateway.Services
{
    public partial class AwsApiGatewayService : SystemIntegrationServiceBase, IAwsApiGatewayService
    {
        public IAwsApiGatewayHelper Helper { get; set; }

        protected AwsApiGatewayService() : base()
        {
        }

        public AwsApiGatewayService(ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            Uri apiGatewayBaseUri,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsApiGatewayHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, apiGatewayBaseUri, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        public AwsApiGatewayService(ILogger logger, IAwsApiGatewayHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
