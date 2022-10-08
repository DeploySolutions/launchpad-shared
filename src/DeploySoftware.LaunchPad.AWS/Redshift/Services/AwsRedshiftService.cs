using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Redshift.Services
{
    public partial class AwsRedshiftService : SystemIntegrationServiceBase, IAwsRedshiftService
    {
        public IAwsRedshiftHelper Helper { get; set; }

        protected AwsRedshiftService() : base()
        {
        }

        public AwsRedshiftService(ILogger logger,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsRedshiftHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        public AwsRedshiftService(ILogger logger, IAwsRedshiftHelper helper) : base(logger)
        {
            Helper = helper;
        }

    }
}
