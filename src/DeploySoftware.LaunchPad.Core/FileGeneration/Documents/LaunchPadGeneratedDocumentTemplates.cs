using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public class LaunchPadGeneratedDocumentTemplates
    {
        public string HeaderFooterTemplateName { get; set; }
        public IList<string> CoverPageTemplateNames { get; set; }
        public IList<string> ClosingPageTemplateNames { get; set; }

        public LaunchPadGeneratedDocumentTemplates()
        {
            HeaderFooterTemplateName = string.Empty;
            CoverPageTemplateNames = new List<string>();
            ClosingPageTemplateNames = new List<string>();
        }
    }
}
