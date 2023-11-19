// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-05-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedObjectInheritance.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface ILaunchPadGeneratedObjectInheritance
    /// </summary>
    public partial interface ILaunchPadGeneratedObjectInheritance
    {

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the fully qualified.
        /// </summary>
        /// <value>The type of the fully qualified.</value>
        public string FullyQualifiedType { get; set; }

        /// <summary>
        /// Gets or sets the name of the assembly fully qualified.
        /// </summary>
        /// <value>The name of the assembly fully qualified.</value>
        public string AssemblyFullyQualifiedName { get; set; }

        /// <summary>
        /// Gets or sets the type of the parent fully qualified.
        /// </summary>
        /// <value>The type of the parent fully qualified.</value>
        string ParentFullyQualifiedType { get; set; }
        /// <summary>
        /// Gets the children fully qualified types.
        /// </summary>
        /// <value>The children fully qualified types.</value>
        IDictionary<string, string> ChildrenFullyQualifiedTypes { get; }
        /// <summary>
        /// Gets or sets the inherits from.
        /// </summary>
        /// <value>The inherits from.</value>
        IDictionary<string, string> InheritsFrom { get; set; }
    }
}