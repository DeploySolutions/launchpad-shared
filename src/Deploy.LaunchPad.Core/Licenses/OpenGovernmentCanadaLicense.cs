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
using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core.Licenses
{
    /// <summary>
    /// Implements the Canadian Open Government license term information.
    /// </summary>
    public partial class OpenGovernmentCanadaLicense : License
    {

        /// <summary>
        /// The name of the license
        /// </summary>
        /// <value>The name.</value>
        public override ElementName Name => new ElementName(
            Deploy_LaunchPad_Core_Resources.Text_OpenGovernmentCanadaLicense_LicenseName);

        /// <summary>
        /// A brief human-readable description of the license
        /// </summary>
        /// <value>The summary.</value>
        public override ElementDescription Description => new ElementDescription(Deploy_LaunchPad_Core_Resources
            .Text_OpenGovernmentCanadaLicense_LicenseDescription);

        /// <summary>
        /// A link to the full terms of the license
        /// </summary>
        /// <value>The full terms URL.</value>
        public override Uri FullTermsUrl => new Uri(Deploy_LaunchPad_Core_Resources.Text_OpenGovernmentCanadaLicense_LicenseTerms);

    }
}
