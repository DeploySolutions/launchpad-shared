// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IDataPoint.cs" company="Deploy Software Solutions, inc.">
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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Domain.Model;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Interface IDataPoint
    /// Extends the <see cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    /// Extends the <see cref="IMayHaveTenant" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <seealso cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    /// <seealso cref="IMayHaveTenant" />
    public partial interface IDataPoint<TPrimaryKey> : ILaunchPadDomainEntity<TPrimaryKey>, IMayHaveTenant
    {
        /// <summary>
        /// Describes the schema (where known) according to which this data is structured.
        /// </summary>
        /// <value>The schema.</value>
        public ISchemaDetails Schema { get; set; }
    }
}
