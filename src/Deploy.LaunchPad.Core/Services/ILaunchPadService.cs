// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="ILaunchPadService.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Castle.Core.Logging;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Core.Services
{
    /// <summary>
    /// Base service interface for LaunchPad
    /// </summary>
    public partial interface ILaunchPadService
    {
        public ElementNameLight Name { get; set; }
        public ElementDescriptionLight Description { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }


    }
}
