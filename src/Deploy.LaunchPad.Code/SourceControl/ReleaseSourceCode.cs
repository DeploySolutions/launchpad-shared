// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-19-2023
// ***********************************************************************
// <copyright file="ReleaseSourceCode.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.SourceControl
{
    /// <summary>
    /// Class ReleaseSourceCode.
    /// Implements the <see cref="Deploy.LaunchPad.Core.SourceControl.ReleaseAssetBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.SourceControl.ReleaseAssetBase" />
    [Serializable]
    public partial class ReleaseSourceCode : ReleaseAssetBase
    {

        /// <summary>
        /// Gets or sets the source code tar download.
        /// </summary>
        /// <value>The source code tar download.</value>
        public virtual Uri SourceCodeTarDownload { get; set; }

        /// <summary>
        /// Gets or sets the source code zip download.
        /// </summary>
        /// <value>The source code zip download.</value>
        public virtual Uri SourceCodeZipDownload { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseSourceCode"/> class.
        /// </summary>
        public ReleaseSourceCode() { }
    }
}
