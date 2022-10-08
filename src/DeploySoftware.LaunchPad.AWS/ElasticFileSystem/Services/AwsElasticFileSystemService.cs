using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.ElasticFileSystem.Services
{
    public partial class AwsElasticFileSystemService : SystemIntegrationServiceBase, IAwsElasticFileSystemService
    {

        public IAwsElasticFileSystemHelper Helper { get; set; }

        protected AwsElasticFileSystemService() : base()
        {
        }

        public AwsElasticFileSystemService(ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsElasticFileSystemHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        public AwsElasticFileSystemService(ILogger logger, IAwsElasticFileSystemHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
