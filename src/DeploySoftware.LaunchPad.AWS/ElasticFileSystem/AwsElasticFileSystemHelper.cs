using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.ElasticFileSystem
{
    public partial class AwsElasticFileSystemHelper : AwsHelperBase, IAwsElasticFileSystemHelper
    {
        public AwsElasticFileSystemHelper() : base()
        {
        }

        public AwsElasticFileSystemHelper(ILogger logger, IConfigurationRoot configurationRoot, string awsRegionEndpointName) : base(logger, configurationRoot, awsRegionEndpointName)
        {

        }

    }
}
