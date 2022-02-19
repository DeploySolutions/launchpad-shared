using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Domain;

namespace DeploySoftware.LaunchPad.AWS.ElasticFileSystem
{
    public interface IAwsElasticFileSystemService : ILaunchPadDomainService
    {
        public IAwsElasticFileSystemHelper Helper { get; set; }
    }
}
