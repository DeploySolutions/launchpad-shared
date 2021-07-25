using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.AWS
{
    public abstract partial class AwsHelperBase
    {

        public ILogger Logger { get; set; }

        protected const string DefaultRegionName = "us-east-1";

        public RegionEndpoint Region { get; set; }

        public AwsHelperBase()
        {
            Logger = NullLogger.Instance; 
            Region = GetRegionEndpoint(DefaultRegionName);
        }

        public AwsHelperBase(ILogger logger)
        {
            Logger = logger; 
            Region = GetRegionEndpoint(DefaultRegionName);
        }


        public AwsHelperBase(string awsRegionEndpointName, ILogger logger)
        {
            Logger = logger; 
            Region = GetRegionEndpoint(awsRegionEndpointName);
        }

        public AWSCredentials GetAwsCredentials(string awsProfileName)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials creds;
            if (chain.TryGetAWSCredentials(awsProfileName, out creds))
            {
                Console.WriteLine("AWS credentials created");
            }
            return creds;
        }


        /// <summary>
        /// Returns an AWS region endpoint from a given endpoint name, or the default region "us-east-1" if invalid/none provided.
        /// </summary>
        /// <param name="awsRegionEndpointSystemName">A valid AWS region endpoint system name.</param>
        /// <returns>A valid AWS Region Endpoint</returns>
        public RegionEndpoint GetRegionEndpoint(string awsRegionEndpointSystemName)
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
                region = RegionEndpoint.GetBySystemName(DefaultRegionName);
            }

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetRegionEndpoint_Logger_Info_RegionName, region.DisplayName, region.SystemName));
            return region;
        }
    }
}
