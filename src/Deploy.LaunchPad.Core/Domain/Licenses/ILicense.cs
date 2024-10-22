// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="ILicense.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Core.Domain
{

    using System;

    /// <summary>
    /// Identifies a license to assist with identifying and remaining compliant with its terms
    /// </summary>
    public partial interface ILicense
    {
        /// <summary>
        /// The name of the license
        /// </summary>
        /// <value>The name.</value>
        public ElementName Name { get; }

        /// <summary>
        /// A brief human-readable description of the license
        /// </summary>
        /// <value>The summary.</value>
        public ElementDescription Description { get; }

        public string Text { get; }

        public string StandardLicenseHeader { get; }

        /// <summary>
        /// A link to the full terms of the license
        /// </summary>
        /// <value>The full terms URL.</value>
        public Uri FullTermsUrl { get; }
    }
}
