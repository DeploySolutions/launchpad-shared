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

        public override string LicenseName => "Open Government License - Canada";

        public override string LicenseDescription => @"Open Government is about making government more accessible to everyone. 
                                        This means giving greater access to government data and information to the Canadian public and the businesses community. 
                                        The Information Provider grants you a worldwide, royalty-free, perpetual, non-exclusive licence to use the Information, 
                                        including for commercial purposes, subject to the license terms. 
                                        This licence is governed by the laws of the province of Ontario and the applicable laws of Canada.";

        public override Uri LicenseTerms => new Uri("https://open.canada.ca/en/open-government-licence-canada");
        
    }
}
