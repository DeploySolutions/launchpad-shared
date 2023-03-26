using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial interface ILicensedThirdPartySoftwareItem
    {
        string Copyright { get; set; }
        string Description { get; set; }
        string LegalName { get; set; }
        Uri MoreInformationUri { get; set; }
        string Name { get; set; }
        SourceControlRepository SourceRepository { get; set; }
        UsageRights UsageRights { get; set; }
        string Version { get; set; }

        DateTime RefreshDate { get; set; }
    }
}