using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.ElasticFileSystem;
using Deploy.LaunchPad.AWS.ElasticFileSystem.Services;
using Deploy.LaunchPad.Core.Abp.Application;
using Microsoft.Extensions.Configuration;

namespace Deploy.LaunchPad.AWS.Abp.ElasticFileSystem.Services
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
