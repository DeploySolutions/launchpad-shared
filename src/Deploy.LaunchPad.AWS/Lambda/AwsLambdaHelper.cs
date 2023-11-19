// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsLambdaHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.Lambda;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.Lambda
{
    /// <summary>
    /// Class AwsLambdaHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.Lambda.AmazonLambdaConfig}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.Lambda.IAwsLambdaHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.Lambda.AmazonLambdaConfig}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.Lambda.IAwsLambdaHelper" />
    public partial class AwsLambdaHelper : AwsHelperBase<AmazonLambdaConfig>, IAwsLambdaHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwsLambdaHelper"/> class.
        /// </summary>
        public AwsLambdaHelper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsLambdaHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsLambdaHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
