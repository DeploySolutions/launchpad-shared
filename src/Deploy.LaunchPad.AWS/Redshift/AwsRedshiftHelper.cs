// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsRedshiftHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.Redshift;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.Redshift
{
    /// <summary>
    /// Class AwsRedshiftHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.Redshift.AmazonRedshiftConfig}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.Redshift.IAwsRedshiftHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.Redshift.AmazonRedshiftConfig}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.Redshift.IAwsRedshiftHelper" />
    public partial class AwsRedshiftHelper : AwsHelperBase<AmazonRedshiftConfig>, IAwsRedshiftHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwsRedshiftHelper"/> class.
        /// </summary>
        public AwsRedshiftHelper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsRedshiftHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsRedshiftHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
