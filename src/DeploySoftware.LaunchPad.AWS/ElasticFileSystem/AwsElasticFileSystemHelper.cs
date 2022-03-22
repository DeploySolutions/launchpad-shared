using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.ElasticFileSystem
{
    public partial class AwsElasticFileSystemHelper : HelperBase, IAwsElasticFileSystemHelper
    {
        public AwsCommonHelper AwsCommonHelper { get; set; }
    }
}
