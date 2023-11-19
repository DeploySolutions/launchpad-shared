// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="SoftwareInfrastructure.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class SoftwareInfrastructure.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.ISoftwareInfrastructure" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.ISoftwareInfrastructure" />
    [Serializable]
    public partial class SoftwareInfrastructure : ISoftwareInfrastructure
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the cloud provider.
        /// </summary>
        /// <value>The cloud provider.</value>
        public virtual CloudProviderEnum CloudProvider { get; set; } = CloudProviderEnum.AWS;

        /// <summary>
        /// Gets or sets the abp framework.
        /// </summary>
        /// <value>The abp framework.</value>
        public virtual AbpFrameworkEnum AbpFramework { get; set; } = AbpFrameworkEnum.Abp;

        /// <summary>
        /// Gets or sets the infrastructure as code frameworks.
        /// </summary>
        /// <value>The infrastructure as code frameworks.</value>
        public virtual IDictionary<string, IInfrastructureAsCodeFramework> InfrastructureAsCodeFrameworks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [search is enabled].
        /// </summary>
        /// <value><c>true</c> if [search is enabled]; otherwise, <c>false</c>.</value>
        public virtual bool SearchIsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [should override parent].
        /// </summary>
        /// <value><c>true</c> if [should override parent]; otherwise, <c>false</c>.</value>
        public bool ShouldOverrideParent { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareInfrastructure"/> class.
        /// </summary>
        public SoftwareInfrastructure()
        {
            Name = AbpFramework.ToString() + "." + CloudProvider.ToString();
            Description = string.Format(
                "This element will use the coding framework '{0}' and will deploy in '{1}'.",
                AbpFramework.ToString(),
                CloudProvider.ToString()
            );
            var comparer = StringComparer.OrdinalIgnoreCase;
            InfrastructureAsCodeFrameworks = new Dictionary<string, IInfrastructureAsCodeFramework>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareInfrastructure"/> class.
        /// </summary>
        /// <param name="cloudProvider">The cloud provider.</param>
        /// <param name="abpFramework">The abp framework.</param>
        public SoftwareInfrastructure(CloudProviderEnum cloudProvider, AbpFrameworkEnum abpFramework)
        {
            CloudProvider = cloudProvider;
            AbpFramework = abpFramework;
            Name = AbpFramework.ToString() + "." + CloudProvider.ToString();
            Description = string.Format(
                "This element will use the coding framework '{0}' and will deploy in '{1}'.",
                AbpFramework.ToString(),
                CloudProvider.ToString()
            );
            var comparer = StringComparer.OrdinalIgnoreCase;
            InfrastructureAsCodeFrameworks = new Dictionary<string, IInfrastructureAsCodeFramework>(comparer);
        }


        /// <summary>
        /// Gets the name of the solution folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetSolutionFolderName()
        {
            return AbpFramework.ToString() + "." + CloudProvider.ToString();
        }

    }
}
