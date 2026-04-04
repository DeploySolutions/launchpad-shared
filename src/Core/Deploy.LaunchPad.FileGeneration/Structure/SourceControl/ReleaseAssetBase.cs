// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="ReleaseAssetBase.cs" company="Deploy Software Solutions, inc.">
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
    /// Class ReleaseAssetBase.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.SourceControl.IReleaseAsset" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.SourceControl.IReleaseAsset" />
    [Serializable]
    public abstract partial class ReleaseAssetBase : IReleaseAsset
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public virtual Uri Uri { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseAssetBase"/> class.
        /// </summary>
        public ReleaseAssetBase() { }
    }
}
