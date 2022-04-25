using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.AWS
{
    public abstract partial class AwsHelperBase : HelperBase, IAwsHelper
    {

        public const string DefaultRegionEndpointName = "us-east-1";
        public const string DefaultLocalAwsProfileName = "default";
        public const bool DefaultShouldUseLocalAwsProfile = false;

        public RegionEndpoint Region { get; set; }

        public string AwsProfileName { get; set; } = DefaultLocalAwsProfileName;

        public bool ShouldUseLocalAwsProfile { get; set; } = DefaultShouldUseLocalAwsProfile;

        public AwsHelperBase() : base()
        {
            Region = GetRegionEndpoint(DefaultRegionEndpointName);
        }

        public AwsHelperBase(ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(DefaultRegionEndpointName);
        }


        public AwsHelperBase(string awsRegionEndpointName, ILogger logger) : base(logger)
        {
            Logger = logger; 
            Region = GetRegionEndpoint(awsRegionEndpointName);
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
        /// Returns an AWS region endpoint from a given endpoint name, or the default region "us-east-1" if invalid/none provided.
        /// </summary>
        /// <param name="awsRegionEndpointSystemName">A valid AWS region endpoint system name.</param>
        /// <returns>A valid AWS Region Endpoint</returns>
        public virtual RegionEndpoint GetRegionEndpoint(string awsRegionEndpointSystemName)
        {
            RegionEndpoint region = null;

            // attempt to load the Region Endpoint from the list of available ones
            if (!string.IsNullOrEmpty(awsRegionEndpointSystemName))
            {
                foreach (var e in RegionEndpoint.EnumerableAllRegions)
                {
                    if (e.Equals(awsRegionEndpointSystemName))
                    {
                        region = e;
                    }
                }
            }

            // if the region is still null, or the string was null or empty previously, use the default region endpoint
            if (region == null)
            {
                region = RegionEndpoint.GetBySystemName(DefaultRegionEndpointName);
            }

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetRegionEndpoint_Logger_Info_RegionName, region.DisplayName, region.SystemName));
            return region;
        }
    }
}
