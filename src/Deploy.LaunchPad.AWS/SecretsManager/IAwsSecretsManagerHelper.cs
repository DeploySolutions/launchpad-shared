// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsSecretsManagerHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.SecretsManager;

namespace Deploy.LaunchPad.AWS.SecretsManager
{
    /// <summary>
    /// Interface IAwsSecretsManagerHelper
    /// Extends the <see cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.SecretsManager.AmazonSecretsManagerConfig}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.SecretsManager.AmazonSecretsManagerConfig}" />
    public partial interface IAwsSecretsManagerHelper : IAwsHelper<AmazonSecretsManagerConfig>
    {

        /// <summary>
        /// Gets the secrets manager client.
        /// </summary>
        /// <value>The secrets manager client.</value>
        public AmazonSecretsManagerClient SecretsManagerClient { get; }

    }
}
