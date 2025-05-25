// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="IAwsApiGatewayService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Services;

namespace Deploy.LaunchPad.AWS.ApiGateway.Services
{
    /// <summary>
    /// Interface IAwsApiGatewayService
    /// Extends the <see cref="ILaunchPadSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="ILaunchPadSystemIntegrationService" />
    public partial interface IAwsApiGatewayService : ILaunchPadSystemIntegrationService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsApiGatewayHelper Helper { get; set; }
    }
}
