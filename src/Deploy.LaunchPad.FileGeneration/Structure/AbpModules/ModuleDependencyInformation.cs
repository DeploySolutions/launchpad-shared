// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-14-2023
// ***********************************************************************
// <copyright file="ModuleDependencyInformation.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.AbpModules
{
    /// <summary>
    /// Class ModuleDependencyInformation.
    /// </summary>
    public partial class ModuleDependencyInformation
    {
        /// <summary>
        /// Gets or sets the base dependencies.
        /// </summary>
        /// <value>The base dependencies.</value>
        public virtual IDictionary<string, string> BaseDependencies { get; set; }

        /// <summary>
        /// Gets or sets the custom dependencies.
        /// </summary>
        /// <value>The custom dependencies.</value>
        public virtual IDictionary<string, string> CustomDependencies { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDependencyInformation"/> class.
        /// </summary>
        public ModuleDependencyInformation()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            BaseDependencies = new Dictionary<string, string>(comparer);
            CustomDependencies = new Dictionary<string, string>(comparer);
        }
    }
}
