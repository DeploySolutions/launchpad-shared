// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="IRelease.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Domain.SourceControl
{
    /// <summary>
    /// Interface IRelease
    /// </summary>
    public partial interface IRelease
    {
        /// <summary>
        /// Gets or sets the assets.
        /// </summary>
        /// <value>The assets.</value>
        public IDictionary<string, IReleaseAsset> Assets { get; set; }
        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        /// <value>The checksum.</value>
        public string Checksum { get; set; }
        /// <summary>
        /// Gets or sets the source code zip.
        /// </summary>
        /// <value>The source code zip.</value>
        public ReleaseSourceCode SourceCodeZip { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        public DateTime ReleaseDate { get; set; }

    }
}