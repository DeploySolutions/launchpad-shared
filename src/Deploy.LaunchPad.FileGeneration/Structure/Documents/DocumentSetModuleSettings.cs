// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DocumentSetModuleSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class DocumentSetModuleSettings.
    /// Implements the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// </summary>
    /// <seealso cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    [Serializable]
    public partial class DocumentSetModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// Contains information on the template(s) that should be used to generate the documents in this set.
        /// If any are listed in the child component(s), those take precedence over these.
        /// </summary>
        /// <value>The templates.</value>
        public virtual LaunchPadGeneratedDocumentTemplates Templates { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentSetModuleSettings"/> class.
        /// </summary>
        public DocumentSetModuleSettings() : base()
        {
            Templates = new LaunchPadGeneratedDocumentTemplates();
        }
    }
}
