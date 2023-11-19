// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsRedshiftHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.Redshift;

namespace Deploy.LaunchPad.AWS.Redshift
{
    /// <summary>
    /// Interface IAwsRedshiftHelper
    /// Extends the <see cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.Redshift.AmazonRedshiftConfig}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.Redshift.AmazonRedshiftConfig}" />
    public partial interface IAwsRedshiftHelper : IAwsHelper<AmazonRedshiftConfig>
    {
    }
}
