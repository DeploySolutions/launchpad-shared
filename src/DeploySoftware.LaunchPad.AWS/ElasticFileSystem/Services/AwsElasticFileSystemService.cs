using DeploySoftware.LaunchPad.Core.Configuration;
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
    }
}
