using Deploy.LaunchPad.FileGeneration.Stages;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class DocumentSetBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// Contains information on the template(s) that should be used to generate the documents in this component.
        /// If templates are included in the module, any listed here take precedence. 
        /// If any are listed in the child Document(s) those take precedence.
        ///
        /// </summary>
        public virtual LaunchPadGeneratedDocumentTemplates Templates { get; set; }

        public DocumentSetBlueprintDefinitionSettings() : base()
        {
            Templates = new LaunchPadGeneratedDocumentTemplates();
        }
    }
}
