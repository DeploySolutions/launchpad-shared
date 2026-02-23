// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="OpenGovernmentCanadaLicense.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core.Licenses
{
    /// <summary>
    /// Implements the Canadian Open Government license term information.
    /// </summary>
    public partial class OpenGovernmentCanadaLicense : License
    {

        public OpenGovernmentCanadaLicense() : base(
           new ElementName(Deploy_LaunchPad_Util_Resources.Text_OpenGovernmentCanadaLicense_LicenseName),
           new Uri(Deploy_LaunchPad_Util_Resources.Text_OpenGovernmentCanadaLicense_LicenseTerms),
           new ElementDescription(Deploy_LaunchPad_Util_Resources
            .Text_OpenGovernmentCanadaLicense_LicenseDescription)
        )
        {

        }
    }
}
