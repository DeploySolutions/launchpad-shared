// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsLambdaService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.Lambda;
using Deploy.LaunchPad.AWS.Lambda.Services;
using Deploy.LaunchPad.Core.Abp.Application;

namespace Deploy.LaunchPad.AWS.Abp.Lambda.Services
{
    /// <summary>
    /// Class AwsLambdaService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsLambdaService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsLambdaService" />
    public partial class AwsLambdaService : LaunchPadAbpSystemIntegrationServiceBase, IAwsLambdaService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsLambdaHelper Helper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsLambdaService"/> class.
        /// </summary>
        public AwsLambdaService() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsLambdaService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public AwsLambdaService(ILogger logger) : base(logger)
        {

        }
    }
}
