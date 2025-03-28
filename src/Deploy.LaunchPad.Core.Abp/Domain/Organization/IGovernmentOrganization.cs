﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="IGovernmentOrganization.cs" company="Deploy Software Solutions, inc.">
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

using Schema.NET;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Interface IGovernmentOrganization
    /// Extends the <see cref="Deploy.LaunchPad.Core.Abp.Domain.IOrganization{TPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Domain.IOrganization{TPrimaryKey}" />
    public partial interface IGovernmentOrganization<TPrimaryKey> : IOrganizationDomainEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        GovernmentOrganization Schema { get; set; }

    }
}
