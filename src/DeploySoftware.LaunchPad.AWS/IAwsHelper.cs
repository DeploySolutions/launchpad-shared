using Amazon;
using Amazon.Runtime;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public interface IAwsHelper<TClientConfig> : IHelper
        where TClientConfig : ClientConfig, new()
    {
        public TClientConfig Config { get; set; }

        public RegionEndpoint Region { 
            get
            {
                return Config.RegionEndpoint;
            }
            set
            {
                if(value!= null)
                {
                    Config.RegionEndpoint = value;
                }
            }
        }

        public AWSCredentials GetAwsCredentialsFromNamedLocalProfile(string awsProfileName);

        public bool TryGetRegionEndpoint(string awsRegionEndpointSystemName, out RegionEndpoint region);

    }
}
