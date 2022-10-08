using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using DeploySoftware.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.ApiGateway.Services
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
