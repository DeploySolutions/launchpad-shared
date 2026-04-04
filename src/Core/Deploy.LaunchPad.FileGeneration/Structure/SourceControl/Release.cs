// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="Release.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    /// <summary>
    /// Class Release.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.SourceControl.IRelease" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.SourceControl.IRelease" />
    [Serializable]
    public partial class Release : IRelease
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        /// <value>The checksum.</value>
        public virtual string Checksum { get; set; }

        /// <summary>
        /// Gets or sets the source code zip.
        /// </summary>
        /// <value>The source code zip.</value>
        public virtual ReleaseSourceCode SourceCodeZip { get; set; }

        /// <summary>
        /// Gets or sets the assets.
        /// </summary>
        /// <value>The assets.</value>
        public virtual IDictionary<string, IReleaseAsset> Assets { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        public virtual DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Release"/> class.
        /// </summary>
        public Release()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Assets = new Dictionary<string, IReleaseAsset>(comparer);
        }
    }
}
