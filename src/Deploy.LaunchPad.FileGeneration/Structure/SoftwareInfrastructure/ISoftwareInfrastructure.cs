// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ISoftwareInfrastructure.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface ISoftwareInfrastructure
    /// </summary>
    public partial interface ISoftwareInfrastructure
    {
        /// <summary>
        /// Gets or sets the cloud provider.
        /// </summary>
        /// <value>The cloud provider.</value>
        public CloudProviderEnum CloudProvider { get; set; }

        /// <summary>
        /// Gets or sets the abp framework.
        /// </summary>
        /// <value>The abp framework.</value>
        public AbpFrameworkEnum AbpFramework { get; set; }

        /// <summary>
        /// Gets or sets the infrastructure as code frameworks.
        /// </summary>
        /// <value>The infrastructure as code frameworks.</value>
        public IDictionary<string, IInfrastructureAsCodeFramework> InfrastructureAsCodeFrameworks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [search is enabled].
        /// </summary>
        /// <value><c>true</c> if [search is enabled]; otherwise, <c>false</c>.</value>
        public bool SearchIsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [should override parent].
        /// </summary>
        /// <value><c>true</c> if [should override parent]; otherwise, <c>false</c>.</value>
        public bool ShouldOverrideParent { get; set; }

        /// <summary>
        /// Gets the name of the solution folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetSolutionFolderName();

    }
}