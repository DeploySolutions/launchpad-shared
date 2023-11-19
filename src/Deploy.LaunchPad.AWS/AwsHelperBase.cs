// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsHelperBase.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Util;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Class AwsHelperBase.
    /// Implements the <see cref="HelperBase" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.IAwsHelper{TClientConfig}" />
    /// </summary>
    /// <typeparam name="TClientConfig">The type of the t client configuration.</typeparam>
    /// <seealso cref="HelperBase" />
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsHelper{TClientConfig}" />
    public abstract partial class AwsHelperBase<TClientConfig> : HelperBase, IAwsHelper<TClientConfig>
        where TClientConfig : ClientConfig, new()
    {

        /// <summary>
        /// The default region endpoint name
        /// </summary>
        public const string DefaultRegionEndpointName = "us-east-1";
        /// <summary>
        /// The default local aws profile name
        /// </summary>
        public const string DefaultLocalAwsProfileName = "default";
        /// <summary>
        /// The default should use local aws profile
        /// </summary>
        public const bool DefaultShouldUseLocalAwsProfile = false;

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public virtual TClientConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>The region.</value>
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

        /// <summary>
        /// Gets or sets the name of the aws profile.
        /// </summary>
        /// <value>The name of the aws profile.</value>
        public virtual string AwsProfileName { get; set; } = DefaultLocalAwsProfileName;

        /// <summary>
        /// Gets or sets a value indicating whether [should use local aws profile].
        /// </summary>
        /// <value><c>true</c> if [should use local aws profile]; otherwise, <c>false</c>.</value>
        public virtual bool ShouldUseLocalAwsProfile { get; set; } = DefaultShouldUseLocalAwsProfile;

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsHelperBase{TClientConfig}"/> class.
        /// </summary>
        public AwsHelperBase() : base()
        {
            Config = new TClientConfig();
            TryGetRegionEndpoint(string.Empty, out RegionEndpoint region);
            Region = region;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsHelperBase{TClientConfig}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
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
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <returns>AWSCredentials.</returns>
        public AWSCredentials GetAwsCredentialsFromNamedLocalProfile(string awsProfileName)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials creds;
            bool didGetCredentials = chain.TryGetAWSCredentials(awsProfileName, out creds);
            if (didGetCredentials)
            {
                AwsProfileName = awsProfileName;
                Logger.Info(string.Format("AWS credentials created from local named profile '{0}'.", awsProfileName));
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
        /// <param name="region">The region.</param>
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
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.SecretHelper_GetRegionEndpoint_Logger_Info_RegionName, region.DisplayName, region.SystemName));
            return false;
        }
    }
}
