using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
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

        public AwsElasticFileSystemHelper(ILogger logger) : base(logger)
        {

        }

    }
}
