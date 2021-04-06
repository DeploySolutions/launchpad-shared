using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedDocumentSetModuleConfiguration : LaunchPadGeneratedConfiguration
    {

        /// <summary>
        /// Contains information on the template(s) that should be used to generate the documents in this set. 
        /// If any are listed in the child component(s), those take precedence over these.
        /// </summary>
        public virtual LaunchPadGeneratedDocumentTemplates Templates {get;set;}

        public LaunchPadGeneratedDocumentSetModuleConfiguration() : base()
        {
            Templates = new LaunchPadGeneratedDocumentTemplates();
        }
    }
}
