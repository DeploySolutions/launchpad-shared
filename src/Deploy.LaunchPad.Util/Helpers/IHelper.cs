// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IHelper.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Castle.Core.Logging;
using System;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Interface IHelper
    /// </summary>
    public partial interface IHelper
    {

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets the description from enum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="shouldReturnOriginalValueIfDescriptionEmpty">if set to <c>true</c> [should return original value if description empty].</param>
        /// <returns>System.String.</returns>
        public string GetDescriptionFromEnum(Enum value, bool shouldReturnOriginalValueIfDescriptionEmpty = true);
    }
}
