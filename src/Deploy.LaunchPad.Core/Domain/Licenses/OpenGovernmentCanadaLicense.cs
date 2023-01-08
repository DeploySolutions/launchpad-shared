using System;

namespace Deploy.LaunchPad.Core.Domain
{
    /// <summary>
    /// Implements the Canadian Open Government license term information.
    /// </summary>
    public class OpenGovernmentCanadaLicense : License
    {

        public override string Name =>
            Deploy_LaunchPad_Core_Resources.Text_OpenGovernmentCanadaLicense_LicenseName;

        public override string Summary => Deploy_LaunchPad_Core_Resources
            .Text_OpenGovernmentCanadaLicense_LicenseDescription;

        public override Uri FullTermsUrl => new Uri(Deploy_LaunchPad_Core_Resources.Text_OpenGovernmentCanadaLicense_LicenseTerms);

    }
}
