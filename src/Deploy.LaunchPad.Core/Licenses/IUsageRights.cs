// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="IUsageRights.cs" company="Deploy Software Solutions, inc.">
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

using System;

namespace Deploy.LaunchPad.Core.Licenses
{
    /// <summary>
    /// Interface IUsageRights
    /// </summary>
    public partial interface IUsageRights
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        string Owner { get; set; }

        /// <summary>
        /// Gets or sets the attribution.
        /// </summary>
        /// <value>The attribution.</value>
        string Attribution { get; set; }

        /// <summary>
        /// Gets or sets the governing license.
        /// </summary>
        /// <value>The governing license.</value>
        ILicense GoverningLicense { get; set; }

        /// <summary>
        /// Gets or sets the project link.
        /// </summary>
        /// <value>The project link.</value>
        Uri ProjectLink { get; set; }
    }
}
