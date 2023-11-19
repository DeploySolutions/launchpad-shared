// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedFolder.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class LaunchPadGeneratedFolder.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadGeneratedObjectBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadGeneratedObjectBase" />
    [Serializable]
    public partial class LaunchPadGeneratedFolder : LaunchPadGeneratedObjectBase
    {
        /// <summary>
        /// Gets or sets the sub folders.
        /// </summary>
        /// <value>The sub folders.</value>
        public virtual IList<LaunchPadGeneratedFolder> SubFolders { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedFolder"/> class.
        /// </summary>
        public LaunchPadGeneratedFolder() : base()
        {
            SubFolders = new List<LaunchPadGeneratedFolder>();
        }
    }
}
