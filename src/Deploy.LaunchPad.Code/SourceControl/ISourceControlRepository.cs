// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="ISourceControlRepository.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Deploy.LaunchPad.Core.Elements;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Code.SourceControl
{
    /// <summary>
    /// Interface ISourceControlRepository
    /// </summary>
    public partial interface ISourceControlRepository
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        ElementName Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        ElementDescription Description { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        Uri Uri { get; set; }

        string LocalFilePath { get; set; }
    }
}