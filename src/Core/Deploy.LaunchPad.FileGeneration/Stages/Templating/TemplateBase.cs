// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="TemplateBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Class TemplateBase.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Stages.ITemplate" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Stages.ITemplate" />
    [Serializable]
    public abstract partial class TemplateBase : ITemplate
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets or sets the template base path.
        /// </summary>
        /// <value>The template base path.</value>
        public virtual string TemplateBasePath { get; set; }
        /// <summary>
        /// Gets or sets the available tokens.
        /// </summary>
        /// <value>The available tokens.</value>
        public IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateBase"/> class.
        /// </summary>
        public TemplateBase()
        {
            Name = string.Empty;
            Key = string.Empty;
            TemplateBasePath = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            AvailableTokens = new Dictionary<string, LaunchPadToken>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateBase"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="templateBasePath">The template base path.</param>
        public TemplateBase(string key, string templateBasePath)
        {
            Name = key;
            Key = key;
            TemplateBasePath = templateBasePath;
            var comparer = StringComparer.OrdinalIgnoreCase;
            AvailableTokens = new Dictionary<string, LaunchPadToken>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateBase"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="templateBasePath">The template base path.</param>
        /// <param name="tokens">The tokens.</param>
        public TemplateBase(string key, string templateBasePath, IDictionary<string, LaunchPadToken> tokens)
        {
            Name = key;
            Key = key;
            TemplateBasePath = templateBasePath;
            AvailableTokens = tokens;
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Equals(TemplateBase other)
        {
            return this.Name.Equals(other.Name) &&
                   this.Key.Equals(other.Key);
        }

    }
}
