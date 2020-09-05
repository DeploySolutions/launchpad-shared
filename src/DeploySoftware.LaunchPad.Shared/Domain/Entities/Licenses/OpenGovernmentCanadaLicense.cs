using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    /// <summary>
    /// Implements the Canadian Open Government license term information.
    /// </summary>
    public class OpenGovernmentCanadaLicense : License
    {

        public override string Name =>
            DeploySoftware_LaunchPad_Shared_Resources.Text_OpenGovernmentCanadaLicense_LicenseName;

        public override string Summary => DeploySoftware_LaunchPad_Shared_Resources
            .Text_OpenGovernmentCanadaLicense_LicenseDescription;

        public override Uri FullTermsUrl => new Uri(DeploySoftware_LaunchPad_Shared_Resources.Text_OpenGovernmentCanadaLicense_LicenseTerms);
        
    }
}
