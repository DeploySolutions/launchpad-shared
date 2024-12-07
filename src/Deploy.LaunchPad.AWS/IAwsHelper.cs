// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon;
using Amazon.Runtime;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Interface IAwsHelper
    /// Extends the <see cref="IHelper" />
    /// </summary>
    /// <typeparam name="TClientConfig">The type of the t client configuration.</typeparam>
    /// <seealso cref="IHelper" />
    public partial interface IAwsHelper<TClientConfig> : IHelper
        where TClientConfig : ClientConfig, new()
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public TClientConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>The region.</value>
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

        /// <summary>
        /// Gets the aws credentials from named local profile.
        /// </summary>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <returns>AWSCredentials.</returns>
        public AWSCredentials GetAwsCredentialsFromNamedLocalProfile(string awsProfileName);

        /// <summary>
        /// Tries the get region endpoint.
        /// </summary>
        /// <param name="awsRegionEndpointSystemName">Name of the aws region endpoint system.</param>
        /// <param name="region">The region.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool TryGetRegionEndpoint(string awsRegionEndpointSystemName, out RegionEndpoint region);

    }
}
