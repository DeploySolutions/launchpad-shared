// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="UsageRights.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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
    /// Class UsageRights.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Domain.IUsageRights" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Domain.IUsageRights" />
    public partial class UsageRights : IUsageRights
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the attribution.
        /// </summary>
        /// <value>The attribution.</value>
        public string Attribution { get; set; }

        /// <summary>
        /// Gets or sets the governing license.
        /// </summary>
        /// <value>The governing license.</value>
        public ILicense GoverningLicense { get; set; }

        /// <summary>
        /// Gets or sets the project link.
        /// </summary>
        /// <value>The project link.</value>
        public Uri ProjectLink { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRights"/> class.
        /// </summary>
        public UsageRights()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRights"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="attribution">The attribution.</param>
        /// <param name="license">The license.</param>
        public UsageRights(string owner, string attribution, ILicense license)
        {
            Owner = owner;
            Attribution = attribution;
            GoverningLicense = license;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsageRights"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="attribution">The attribution.</param>
        /// <param name="license">The license.</param>
        /// <param name="projectLink">The project link.</param>
        public UsageRights(string owner, string attribution, ILicense license, Uri projectLink)
        {
            Owner = owner;
            Attribution = attribution;
            GoverningLicense = license;
            ProjectLink = projectLink;
        }

    }
}
