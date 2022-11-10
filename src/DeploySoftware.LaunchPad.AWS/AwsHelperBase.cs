using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;

namespace DeploySoftware.LaunchPad.AWS
{
    public abstract partial class AwsHelperBase<TClientConfig> : HelperBase, IAwsHelper<TClientConfig>
        where TClientConfig : ClientConfig, new()
    {

        public const string DefaultRegionEndpointName = "us-east-1";
        public const string DefaultLocalAwsProfileName = "default";
        public const bool DefaultShouldUseLocalAwsProfile = false;

        public virtual TClientConfig Config { get; set; }

        public virtual RegionEndpoint Region
        {
            get { return Config.RegionEndpoint; }
            set
            {
                if (value != null)
                {
                    Config.RegionEndpoint = value;
                }
            }
        }

        public virtual string AwsProfileName { get; set; } = DefaultLocalAwsProfileName;

        public virtual bool ShouldUseLocalAwsProfile { get; set; } = DefaultShouldUseLocalAwsProfile;

        public AwsHelperBase() : base()
        {
            Config = new TClientConfig();
            TryGetRegionEndpoint(string.Empty, out RegionEndpoint region);
            Region = region;
        }

        public AwsHelperBase(ILogger logger, string awsRegionEndpointName) : base(logger)
        {
            Config = new TClientConfig();
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
        }


        /// <summary>
        /// This attempts to return AWS Credentials from a specific AWS profile. If left empty, it will try to load from a [default] local credential.
        /// However, this method should really only be used when attempting to load a specific credential, as otherwise the normal credential resolution order
        /// specified here will apply anyway https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/creds-assign.html 
        /// </summary>
        /// <param name="awsProfileName"></param>
        /// <returns></returns>
        public AWSCredentials GetAwsCredentialsFromNamedLocalProfile(string awsProfileName)
        {            
            var chain = new CredentialProfileStoreChain();
            AWSCredentials creds;
            bool didGetCredentials = chain.TryGetAWSCredentials(awsProfileName, out creds);
            if (didGetCredentials)
            {
                AwsProfileName = awsProfileName;
                Logger.Info(string.Format( "AWS credentials created from local named profile '{0}'.", awsProfileName));
            }
            else
            {
                Logger.Info(string.Format("AWS credentials could not be created from local named profile '{0}', does it exist?", awsProfileName));
            }
            return creds;
        }


        /// <summary>
        /// Returns an AWS region endpoint from a given endpoint name, or the default region if invalid/none provided.
        /// </summary>
        /// <param name="awsRegionEndpointSystemName">A valid AWS region endpoint system name.</param>
        /// <returns>A valid AWS Region Endpoint</returns>
        public virtual bool TryGetRegionEndpoint(string awsRegionEndpointSystemName, out RegionEndpoint region)
        {
            // attempt to load the Region Endpoint from the list of available ones
            if (!string.IsNullOrEmpty(awsRegionEndpointSystemName))
            {
                foreach (var e in RegionEndpoint.EnumerableAllRegions)
                {
                    if (e.SystemName.ToLower().Equals(awsRegionEndpointSystemName))
                    {
                        region = e;
                        return true;
                    }
                }
            }

            // use the default region endpoint
            region = RegionEndpoint.GetBySystemName(DefaultRegionEndpointName);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetRegionEndpoint_Logger_Info_RegionName, region.DisplayName, region.SystemName));
            return false;
        }
    }
}
