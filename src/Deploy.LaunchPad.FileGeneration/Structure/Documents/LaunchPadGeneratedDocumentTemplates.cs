using Deploy.LaunchPad.FileGeneration.Stages;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class LaunchPadGeneratedDocumentTemplates : TemplateBase
    {
        public virtual string HeaderFooterTemplateName { get; set; }
        public virtual IList<string> CoverPageTemplateNames { get; set; }
        public virtual IList<string> ClosingPageTemplateNames { get; set; }

        public LaunchPadGeneratedDocumentTemplates()
        {
            HeaderFooterTemplateName = string.Empty;
            CoverPageTemplateNames = new List<string>();
            ClosingPageTemplateNames = new List<string>();
        }
    }
}
