using Amazon;
using Amazon.Runtime;
using Deploy.LaunchPad.Core.Util;

namespace Deploy.LaunchPad.AWS
{
    public interface IAwsHelper<TClientConfig> : IHelper
        where TClientConfig : ClientConfig, new()
    {
        public TClientConfig Config { get; set; }

        public RegionEndpoint Region
        {
            get
            {
                return Config.RegionEndpoint;
            }
            set
            {
                if (value != null)
                {
                    Config.RegionEndpoint = value;
                }
            }
        }

        public AWSCredentials GetAwsCredentialsFromNamedLocalProfile(string awsProfileName);

        public bool TryGetRegionEndpoint(string awsRegionEndpointSystemName, out RegionEndpoint region);

    }
}
