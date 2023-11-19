// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="InfrastructureAsCodeFramework.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class InfrastructureAsCodeFramework.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.IInfrastructureAsCodeFramework" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.IInfrastructureAsCodeFramework" />
    [Serializable]
    public partial class InfrastructureAsCodeFramework : IInfrastructureAsCodeFramework
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public virtual InfrastructureAsCodeFrameworkTypeEnum Type { get; set; } = InfrastructureAsCodeFrameworkTypeEnum.Terraform;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfrastructureAsCodeFramework"/> class.
        /// </summary>
        public InfrastructureAsCodeFramework()
        {

        }
    }
}
