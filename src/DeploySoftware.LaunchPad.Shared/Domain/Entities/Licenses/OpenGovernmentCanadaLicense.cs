using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Shared.Domain.Licenses
{
    /// <summary>
    /// Implements the Canadian Open Government license term information.
    /// </summary>
    public class OpenGovernmentCanadaLicense : License
    {

        public override string LicenseName =>
            DeploySoftware_LaunchPad_Shared_Resources.Text_OpenGovernmentCanadaLicense_LicenseName;

        public override string LicenseDescription => DeploySoftware_LaunchPad_Shared_Resources
            .Text_OpenGovernmentCanadaLicense_LicenseDescription;

        public override Uri LicenseTerms => new Uri(DeploySoftware_LaunchPad_Shared_Resources.Text_OpenGovernmentCanadaLicense_LicenseTerms);
        
    }
}
