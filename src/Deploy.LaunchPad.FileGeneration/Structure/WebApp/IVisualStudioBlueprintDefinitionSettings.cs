// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-23-2023
// ***********************************************************************
// <copyright file="IVisualStudioBlueprintDefinitionSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages.Defining;
using Deploy.LaunchPad.FileGeneration.Structure.WebApp;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface IVisualStudioBlueprintDefinitionSettings
    /// </summary>
    public partial interface IVisualStudioBlueprintDefinitionSettings
    {
        /// <summary>
        /// Gets or sets the application service definitions.
        /// </summary>
        /// <value>The application service definitions.</value>
        IDictionary<string, LaunchPadGeneratedObjectBase> AppServiceDefinitions { get; set; }
        /// <summary>
        /// Gets or sets the application settings.
        /// </summary>
        /// <value>The application settings.</value>
        ILaunchPadGeneratedAppSettings AppSettings { get; set; }
        /// <summary>
        /// Gets or sets the base application service class.
        /// </summary>
        /// <value>The base application service class.</value>
        string BaseAppServiceClass { get; set; }
        /// <summary>
        /// Gets or sets the base application service class annotations.
        /// </summary>
        /// <value>The base application service class annotations.</value>
        string BaseAppServiceClassAnnotations { get; set; }
        /// <summary>
        /// Gets or sets the base namespace.
        /// </summary>
        /// <value>The base namespace.</value>
        string BaseNamespace { get; set; }
        /// <summary>
        /// Gets or sets the custom headers.
        /// </summary>
        /// <value>The custom headers.</value>
        ICustomHttpHeaders CustomHeaders { get; set; }
        /// <summary>
        /// Gets or sets the domain entity definitions.
        /// </summary>
        /// <value>The domain entity definitions.</value>
        IDictionary<string, LaunchPadGeneratedObjectBase> DomainEntityDefinitions { get; set; }
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        string ProjectName { get; set; }
        /// <summary>
        /// Gets or sets the property sets.
        /// </summary>
        /// <value>The property sets.</value>
        IDictionary<string, LaunchPadGeneratedDtoPropertySet> PropertySets { get; set; }
        /// <summary>
        /// Gets or sets the name of the sub folder.
        /// </summary>
        /// <value>The name of the sub folder.</value>
        string SubFolderName { get; set; }
        /// <summary>
        /// Gets or sets the name of the visual studio solution.
        /// </summary>
        /// <value>The name of the visual studio solution.</value>
        string VisualStudioSolutionName { get; set; }
    }
}