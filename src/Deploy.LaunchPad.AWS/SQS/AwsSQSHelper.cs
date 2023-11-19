﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsSQSHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.SQS;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.SQS
{
    /// <summary>
    /// Class AwsSQSHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.SQS.AmazonSQSConfig}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.SQS.IAwsSQSHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.SQS.AmazonSQSConfig}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.SQS.IAwsSQSHelper" />
    public partial class AwsSQSHelper : AwsHelperBase<AmazonSQSConfig>, IAwsSQSHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSQSHelper"/> class.
        /// </summary>
        public AwsSQSHelper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSQSHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsSQSHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
