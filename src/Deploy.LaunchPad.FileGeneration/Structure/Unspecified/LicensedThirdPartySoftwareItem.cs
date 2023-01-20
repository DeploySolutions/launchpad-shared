using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// This class represents a licensed third party or open source item (element, file, component, library, assembly, etc).
    /// It allows us to provide appropriate usage and credit information while generating code.
    /// </summary>
    [Serializable]
    public partial class LicensedThirdPartySoftwareItem : ILicensedThirdPartySoftwareItem
    {
        public virtual string Name { get; set; }

        public virtual string LegalName { get; set; }


        public virtual string Version { get; set; }

        public virtual string Description { get; set; } = "";


        public virtual string Copyright { get; set; } = "";

        public virtual SourceControlRepository SourceRepository { get; set; }

        public virtual UsageRights UsageRights { get; set; }

        public virtual Uri MoreInformationUri { get; set; }

        public LicensedThirdPartySoftwareItem(string name)
        {
            Name = name;
            LegalName = name;
            SourceRepository = new SourceControlRepository();
            UsageRights = new UsageRights();
        }

        public LicensedThirdPartySoftwareItem(string name, string version)
        {
            Name = name;
            LegalName = name;
            SourceRepository = new SourceControlRepository();
            UsageRights = new UsageRights();
            Version = version;
        }


    }
}
