// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-07-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedObject.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface ILaunchPadGeneratedObject
    /// </summary>
    public partial interface ILaunchPadGeneratedObject
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the type of the identifier.
        /// </summary>
        /// <value>The type of the identifier.</value>
        string IdType { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        /// <value>The abbreviation.</value>
        string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the name prefix.
        /// </summary>
        /// <value>The name prefix.</value>
        string NamePrefix { get; set; }
        /// <summary>
        /// Gets or sets the name suffix.
        /// </summary>
        /// <value>The name suffix.</value>
        string NameSuffix { get; set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public GitHubRepository Repository { get; set; }
        /// <summary>
        /// Gets or sets the inheritance.
        /// </summary>
        /// <value>The inheritance.</value>
        public ILaunchPadGeneratedObjectInheritance Inheritance { get; set; }

        /// <summary>
        /// Contains a dictionary of Templates belonging to this object, keyed by the template name
        /// </summary>
        /// <value>The available templates.</value>
        public IDictionary<string, TemplateBase> AvailableTemplates { get; set; }

        /// <summary>
        /// Contains a dictionary of Tokens belonging to this object, keyed by the token name
        /// </summary>
        /// <value>The available tokens.</value>
        public IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }

    }
}