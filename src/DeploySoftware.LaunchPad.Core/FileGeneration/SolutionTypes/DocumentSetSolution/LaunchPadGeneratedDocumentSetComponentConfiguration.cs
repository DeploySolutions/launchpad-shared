using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedDocumentSetComponentConfiguration : LaunchPadGeneratedConfiguration
    {

        /// <summary>
        /// Contains information on the template(s) that should be used to generate the documents in this component.
        /// If templates are included in the module, any listed here take precedence. 
        /// If any are listed in the child Document(s) those take precedence.
        ///
        /// </summary>
        public virtual LaunchPadGeneratedDocumentTemplates Templates {get;set;}

        public LaunchPadGeneratedDocumentSetComponentConfiguration() : base()
        {
            Templates = new LaunchPadGeneratedDocumentTemplates();
        }
    }
}
