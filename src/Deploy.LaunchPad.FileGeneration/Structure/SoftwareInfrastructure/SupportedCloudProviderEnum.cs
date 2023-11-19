// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="SupportedCloudProviderEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Enum CloudProviderEnum
    /// </summary>
    public enum CloudProviderEnum
    {
        /// <summary>
        /// The aws
        /// </summary>
        AWS = 0,
        /// <summary>
        /// The azure
        /// </summary>
        Azure = 1,
        /// <summary>
        /// The on premise
        /// </summary>
        OnPremise = 2
    }
}
