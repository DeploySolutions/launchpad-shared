// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsSecretVault.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Code.Config;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Class AwsSecretVault.
    /// Implements the <see cref="SecretVaultBase" />
    /// Implements the <see cref="ISecretVault" />
    /// </summary>
    /// <seealso cref="SecretVaultBase" />
    /// <seealso cref="ISecretVault" />
    public partial class AwsSecretVault : SecretVaultBase, ISecretVault
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretVault"/> class.
        /// </summary>
        public AwsSecretVault() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretVault"/> class.
        /// </summary>
        /// <param name="arn">The arn.</param>
        public AwsSecretVault(string arn) : base()
        {
            Name = arn;
            VaultId = arn;
            ProviderId = "AmazonSecretsManager";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretVault"/> class.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="name">The name.</param>
        public AwsSecretVault(string arn, string name) : base()
        {
            Name = name;
            VaultId = arn;
            ProviderId = "AmazonSecretsManager";
        }

    }
}
