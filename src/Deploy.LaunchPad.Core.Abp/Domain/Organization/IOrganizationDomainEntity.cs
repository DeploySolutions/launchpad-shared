﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IOrganization.cs" company="Deploy Software Solutions, inc.">
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Domain.Model;
using Schema.NET;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Interface IOrganization
    /// Extends the <see cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <seealso cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    public partial interface IOrganizationDomainEntity<TPrimaryKey> : 
        ILaunchPadDomainEntity<TPrimaryKey>, ILaunchPadOrganization
    {
        

    }
}
