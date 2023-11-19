// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsLambdaHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.Lambda;

namespace Deploy.LaunchPad.AWS.Lambda
{
    /// <summary>
    /// Interface IAwsLambdaHelper
    /// Extends the <see cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.Lambda.AmazonLambdaConfig}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.Lambda.AmazonLambdaConfig}" />
    public partial interface IAwsLambdaHelper : IAwsHelper<AmazonLambdaConfig>
    {
    }
}
