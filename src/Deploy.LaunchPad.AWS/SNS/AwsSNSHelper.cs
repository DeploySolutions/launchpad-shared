// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsSNSHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.SimpleNotificationService;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.SNS
{
    /// <summary>
    /// Class AwsSNSHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceConfig}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.SNS.IAwsSNSHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceConfig}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.SNS.IAwsSNSHelper" />
    public partial class AwsSNSHelper : AwsHelperBase<AmazonSimpleNotificationServiceConfig>, IAwsSNSHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSNSHelper"/> class.
        /// </summary>
        public AwsSNSHelper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSNSHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsSNSHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }
    }
}
