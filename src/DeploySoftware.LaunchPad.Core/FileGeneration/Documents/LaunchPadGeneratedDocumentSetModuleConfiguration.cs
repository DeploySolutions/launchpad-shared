using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedDocumentSetModuleConfiguration
    {

        /// <summary>
        /// The name of the Document Set Solution in which this generated module will belong.
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// The file path to the root of the document set solution on the file system.
        /// </summary>
        public string SolutionRootFilePath { get; set; }

        /// <summary>
        /// Contains information on the template(s) that should be used to generate the documents in this set.
        /// If none are listed, templates may be applied if listed in the child Document(s) (again, if any are listed).
        /// </summary>
        public LaunchPadGeneratedDocumentTemplates Templates {get;set;}

        public LaunchPadGeneratedDocumentSetModuleConfiguration() : base()
        {
            SolutionName = string.Empty;
            SolutionRootFilePath = string.Empty;
            Templates = new LaunchPadGeneratedDocumentTemplates();
        }
    }
}
