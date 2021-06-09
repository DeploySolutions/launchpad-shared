using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public class CloudInfrastructure
    {

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
        
        public SupportedCloudProviderEnum CloudProvider { get; set; }

        public CloudInfrastructure()
        {
            CloudProvider = SupportedCloudProviderEnum.AmazonWebServices;
            Name = CloudProvider.ToString();
            Description = string.Empty;
        }

        public CloudInfrastructure(SupportedCloudProviderEnum cloudProvider)
        {
            CloudProvider = cloudProvider;
            Name = CloudProvider.ToString();
            Description = string.Empty;
        }

    }
}
