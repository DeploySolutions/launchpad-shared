// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-23-2023
// ***********************************************************************
// <copyright file="ICustomHttpHeaders.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure.WebApp
{
    /// <summary>
    /// Interface ICustomHttpHeaders
    /// </summary>
    public partial interface ICustomHttpHeaders
    {
        /// <summary>
        /// Gets or sets the custom HTTP headers to add.
        /// </summary>
        /// <value>The custom HTTP headers to add.</value>
        IDictionary<string, string> CustomHttpHeadersToAdd { get; set; }
        /// <summary>
        /// Gets or sets the custom HTTP headers to remove.
        /// </summary>
        /// <value>The custom HTTP headers to remove.</value>
        HashSet<string> CustomHttpHeadersToRemove { get; set; }
    }
}