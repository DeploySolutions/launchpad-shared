// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="AddPackageReferenceToVsProjectInstruction.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Stages.Building
{
    /// <summary>
    /// Class AddPackageReferenceToVsProjectInstruction.
    /// </summary>
    [Serializable]
    public partial class AddPackageReferenceToVsProjectInstruction
    {
        /// <summary>
        /// Gets or sets the csproj file path.
        /// </summary>
        /// <value>The csproj file path.</value>
        public virtual string CsprojFilePath { get; set; }

        /// <summary>
        /// Gets or sets the include.
        /// </summary>
        /// <value>The include.</value>
        public virtual string Include { get; set; }

        /// <summary>
        /// Gets or sets the include after.
        /// </summary>
        /// <value>The include after.</value>
        public virtual string IncludeAfter { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual string Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPackageReferenceToVsProjectInstruction"/> class.
        /// </summary>
        public AddPackageReferenceToVsProjectInstruction()
        {   

        }

    }
}
