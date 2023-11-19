// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-23-2023
// ***********************************************************************
// <copyright file="VisualStudioBlueprintDefinitionSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Stages.Defining;
using Deploy.LaunchPad.FileGeneration.Structure.WebApp;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class VisualStudioBlueprintDefinitionSettings.
    /// Implements the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.IVisualStudioBlueprintDefinitionSettings" />
    /// </summary>
    /// <seealso cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.IVisualStudioBlueprintDefinitionSettings" />
    [Serializable]
    public partial class VisualStudioBlueprintDefinitionSettings :
        LaunchPadGeneratedObjectBlueprintDefinitionSettings, IVisualStudioBlueprintDefinitionSettings
    {
        /// <summary>
        /// Gets or sets the name of the visual studio solution.
        /// </summary>
        /// <value>The name of the visual studio solution.</value>
        /// <font color="red">Badly formed XML comment.</font>
        public virtual string VisualStudioSolutionName { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        /// <value>The base namespace.</value>
        public virtual string BaseNamespace { get; set; }

        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        /// <value>The name of the project.</value>
        public virtual string ProjectName { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        /// <value>The name of the sub folder.</value>
        public virtual string SubFolderName { get; set; }

        /// <summary>
        /// The app service base class from which all app services inherit.
        /// </summary>
        /// <value>The base application service class.</value>
        public virtual string BaseAppServiceClass { get; set; }

        /// <summary>
        /// The annotations on the app service base class
        /// </summary>
        /// <value>The base application service class annotations.</value>
        public virtual string BaseAppServiceClassAnnotations { get; set; }

        /// <summary>
        /// Contains a dictionary of Property Sets belonging to this component, keyed by the property set id
        /// </summary>
        /// <value>The property sets.</value>
        public virtual IDictionary<string, LaunchPadGeneratedDtoPropertySet> PropertySets { get; set; }

        /// <summary>
        /// Contains a dictionary of Domain Entity definitions belonging to this component, keyed by the domain entity id
        /// </summary>
        /// <value>The domain entity definitions.</value>
        public virtual IDictionary<string, LaunchPadGeneratedObjectBase> DomainEntityDefinitions { get; set; }

        /// <summary>
        /// Contains a dictionary of Application Service definitions belonging to this component, keyed by the domain entity id
        /// </summary>
        /// <value>The application service definitions.</value>
        public virtual IDictionary<string, LaunchPadGeneratedObjectBase> AppServiceDefinitions { get; set; }

        /// <summary>
        /// Contains a dictionary of custom HTTP Headers to add or remove from the component
        /// </summary>
        /// <value>The custom headers.</value>
        public virtual ICustomHttpHeaders CustomHeaders { get; set; }

        /// <summary>
        /// Contains the appsettings JSON elements belonging to this component
        /// </summary>
        /// <value>The application settings.</value>
        public virtual ILaunchPadGeneratedAppSettings AppSettings { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudioBlueprintDefinitionSettings"/> class.
        /// </summary>
        public VisualStudioBlueprintDefinitionSettings() : base()
        {
            VisualStudioSolutionName = string.Empty;
            ProjectName = string.Empty;
            SubFolderName = string.Empty;
            BaseNamespace = string.Empty;
            BaseAppServiceClass = string.Empty;
            BaseAppServiceClassAnnotations = string.Empty;
            CustomHeaders = new CustomHttpHeaders();
            var comparer = StringComparer.OrdinalIgnoreCase;
            PropertySets = new Dictionary<string, LaunchPadGeneratedDtoPropertySet>(comparer);
            DomainEntityDefinitions = new Dictionary<string, LaunchPadGeneratedObjectBase>(comparer);
            AppServiceDefinitions = new Dictionary<string, LaunchPadGeneratedObjectBase>(comparer);
        }
    }
}
