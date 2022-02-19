using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// This class represents a licensed third party or open source item (element, file, component, library, assembly, etc).
    /// It allows us to provide appropriate usage and credit information while generating code.
    /// </summary>
    [Serializable]   
    public partial class LicensedThirdPartyItem
    {
        public string FriendlyName { get; set; }
        public string LegalName { get; set; }

        public string Copyright { get; set; }

        public string Description { get; set; }

        public Uri SourceRepositoryUri { get; set; }

        public string SourceRepositoryName { get; set; }

        public string LicenseFriendlyName { get; set; }

        public string LicenseLegalName { get; set; }

        public Uri LicenseLegalUri { get; set; }

        public Uri MoreInformationUri { get; set; }

        public LicensedThirdPartyItem()
        {
            FriendlyName = string.Empty;
            Copyright = string.Empty;
            LegalName = string.Empty;
            Description = string.Empty;
            SourceRepositoryName = string.Empty;
            LicenseFriendlyName = string.Empty;
            LicenseLegalName = string.Empty;
        }


    }
}
