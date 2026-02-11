// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="License.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

namespace Deploy.LaunchPad.Domain.Licenses
{
    using Deploy.LaunchPad.Util;
    using System;

    /// <summary>
    /// Identifies a license to assist with identifying and remaining compliant with its terms
    /// </summary>
    [Serializable]
    public partial class License : ILicense
    {
        /// <summary>
        /// The name of the license
        /// </summary>
        /// <value>The name.</value>
        public virtual ElementName Name { get; init; }


        /// <summary>
        /// The Description of the license
        /// </summary>
        /// <value>The Description.</value>
        public virtual ElementDescription Description { get; set; }

        /// <summary>
        /// The human-readable text of the license (refer to the FullTermsUrl for the legally binding/full text of the license)
        /// </summary>
        /// <value>The text.</value>
        public virtual string Text { get; set; } = string.Empty;

        /// <summary>
        /// Should only include text intended to be put in the header of source files or other files as specified in the license or license appendix when specifically delineated. 
        /// Indicate if there is any variation in the header(i.e. for files developed by a contributor versus when applying license to original work)
        /// Do not include NOTICE info intended for a separate notice file
        /// Leave this field blank if there is no standard header as specifically defined in the license
        /// </summary>
        public virtual string StandardLicenseHeader { get; set; } = string.Empty;

        /// <summary>
        /// A link to the legally binding/full text of the license
        /// </summary>
        /// <value>The full terms URL.</value>
        public virtual Uri FullTermsUrl { get; init; }

        /// <summary>
        /// The "family" of the license to help group similar licenses, for instance "Creative Commons" or "copyleft"
        /// </summary>
        /// <value>The family.</value>
        public virtual string Family { get; set; } = string.Empty;

        /// <summary>
        /// The official maintaining organization or individual for the license
        /// </summary>
        /// <value>The summary.</value>
        public virtual string Maintainer { get; set; } = string.Empty;

        /// <summary>
        /// Open source licenses are licenses that comply with the Open Source Definition – in brief, they allow software to be freely used, modified, and shared. To be approved by the Open Source Initiative (also known as the OSI) a license must go through the Open Source Initiative’s license review process.
        /// True if this license is OSI approved as listed at https://opensource.org/licenses, otherwise blank if not known.
        /// </summary>
        public virtual bool? IsOsiApproved { get; set; }

        /// <summary>
        /// True if listed as Free/Libre by the Free Software Foundation on their website: https://www.gnu.org/licenses/license-list.en.html, False if listed as Not Free. Blank if not listed.
        /// </summary>
        public virtual bool? IsFreeLibre { get; set; }

        protected License()
        {
        }
        
        public License(ElementName name, Uri fullTermsUri)
        {
            Guard.Against<ArgumentNullException>(name == null, "Name cannot be null.");
            Guard.Against<ArgumentNullException>(fullTermsUri == null, "FullTermsUri cannot be null.");
            Name = name;
            FullTermsUrl = fullTermsUri;
        }

        public License(ElementName name, Uri fullTermsUri, ElementDescription description)
        {
            Guard.Against<ArgumentNullException>(name == null, "Name cannot be null.");
            Guard.Against<ArgumentNullException>(fullTermsUri == null, "FullTermsUri cannot be null.");
            Name = name;
            FullTermsUrl = fullTermsUri;
            Description = description;

        }
    }
}
