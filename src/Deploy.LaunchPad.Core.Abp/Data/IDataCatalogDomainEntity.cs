// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IDataCatalogue.cs" company="Deploy Software Solutions, inc.">
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


using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Entities;
using Deploy.LaunchPad.Data;
using System.Collections.Generic;
using IMayHaveTenant = Deploy.LaunchPad.Core.Metadata.IMayHaveTenant;

namespace Deploy.LaunchPad.Core.Abp.Data
{
    /// <summary>
    /// Interface IDataCatalogue
    /// Extends the <see cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    /// Extends the <see cref="IMayHaveTenant" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <seealso cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    /// <seealso cref="IMayHaveTenant" />
    public partial interface IDataCatalogDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat>  : 
        ILaunchPadDataCatalog<TDictionaryKey, TSchemaFormat>, 
        ILaunchPadDomainEntity<TPrimaryKey>, IMayHaveTenant
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {


    }
}
