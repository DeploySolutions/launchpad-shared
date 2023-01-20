﻿using Deploy.LaunchPad.FileGeneration.Stages;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class DocumentSetModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// Contains information on the template(s) that should be used to generate the documents in this set. 
        /// If any are listed in the child component(s), those take precedence over these.
        /// </summary>
        public virtual LaunchPadGeneratedDocumentTemplates Templates { get; set; }

        public DocumentSetModuleSettings() : base()
        {
            Templates = new LaunchPadGeneratedDocumentTemplates();
        }
    }
}