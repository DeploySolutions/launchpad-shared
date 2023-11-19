// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsSecretsManagerHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.SecretsManager;
using Castle.Core.Logging;
using Newtonsoft.Json;

namespace Deploy.LaunchPad.AWS.SecretsManager
{
    /// <summary>
    /// Class AwsSecretsManagerHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.SecretsManager.AmazonSecretsManagerConfig}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.SecretsManager.IAwsSecretsManagerHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.SecretsManager.AmazonSecretsManagerConfig}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.SecretsManager.IAwsSecretsManagerHelper" />
    public partial class AwsSecretsManagerHelper : AwsHelperBase<AmazonSecretsManagerConfig>, IAwsSecretsManagerHelper
    {
        /// <summary>
        /// The aws client
        /// </summary>
        protected AmazonSecretsManagerClient _awsClient;

        /// <summary>
        /// Gets the secrets manager client.
        /// </summary>
        /// <value>The secrets manager client.</value>
        [JsonIgnore]
        public AmazonSecretsManagerClient SecretsManagerClient { get { return _awsClient; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerHelper"/> class.
        /// </summary>
        public AwsSecretsManagerHelper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsSecretsManagerHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {
            _awsClient = new AmazonSecretsManagerClient(Region);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="awsClient">The aws client.</param>
        public AwsSecretsManagerHelper(ILogger logger, string awsRegionEndpointName, AmazonSecretsManagerClient awsClient) : base(logger, awsRegionEndpointName)
        {
            _awsClient = awsClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="awsClient">The aws client.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        public AwsSecretsManagerHelper(ILogger logger, string awsRegionEndpointName, AmazonSecretsManagerClient awsClient, string localAwsProfileName) : base(logger, awsRegionEndpointName)
        {
            AwsProfileName = localAwsProfileName;
            _awsClient = awsClient;
        }
    }
}
