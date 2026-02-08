// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsSNSHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.SimpleNotificationService;
using Amazon.SimpleSystemsManagement;

namespace Deploy.LaunchPad.AWS.SSM
{
    /// <summary>
    /// Interface IAwsSNSHelper
    /// Extends the <see cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceConfig}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceConfig}" />
    public partial interface IAwsSsmHelper : IAwsHelper<AmazonSimpleSystemsManagementConfig>
    {
    }
}
