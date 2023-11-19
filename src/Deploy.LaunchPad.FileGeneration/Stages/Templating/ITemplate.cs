// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ITemplate.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Interface ITemplate
    /// </summary>
    public partial interface ITemplate
    {
        /// <summary>
        /// Gets or sets the available tokens.
        /// </summary>
        /// <value>The available tokens.</value>
        IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        string Key { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the template base path.
        /// </summary>
        /// <value>The template base path.</value>
        string TemplateBasePath { get; set; }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Equals(TemplateBase other);
    }
}