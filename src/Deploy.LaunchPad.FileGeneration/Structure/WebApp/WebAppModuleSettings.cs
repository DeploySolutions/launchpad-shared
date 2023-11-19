// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-15-2023
// ***********************************************************************
// <copyright file="WebAppModuleSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Stages.Defining;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class WebAppModuleSettings.
    /// Implements the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// </summary>
    /// <seealso cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    [Serializable]
    public partial class WebAppModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {
        /// <summary>
        /// The name of the web app
        /// </summary>
        /// <value>The name of the web application.</value>
        public virtual string WebAppName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the visual studio solution.
        /// </summary>
        /// <value>The name of the visual studio solution.</value>
        /// <font color="red">Badly formed XML comment.</font>
        public virtual string VisualStudioSolutionName { get; set; } = string.Empty;

        /// <summary>
        /// The base namespace of the generated items in all the projects.
        /// </summary>
        /// <value>The base namespace.</value>
        public virtual string BaseNamespace { get; set; } = string.Empty;

        /// <summary>
        /// Contains the appsettings JSON elements belonging to this component
        /// </summary>
        /// <value>The application settings.</value>
        public virtual ILaunchPadGeneratedAppSettings AppSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAppModuleSettings"/> class.
        /// </summary>
        public WebAppModuleSettings() : base()
        {
        }
    }
}
