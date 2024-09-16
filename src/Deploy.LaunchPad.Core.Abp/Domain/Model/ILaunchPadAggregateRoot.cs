// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="ILaunchPadAggregateRoot.cs" company="Deploy Software Solutions, inc.">
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
using Abp.Domain.Entities.Auditing;
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Domain.Model;
using System;

namespace Deploy.LaunchPad.Core.Abp.Domain.Model
{

    /// <summary>
    /// Marks any object as an Aggregate Root/Domain Entity that can be manipulated by the LaunchPad platform.
    /// Each entity is uniquely identified by its ID, and contains a
    /// set of <see cref="MetadataInformation">MetadataInformation</see>.
    /// Each entity also implements ASP.NET Boilerplate's IAggregateRoot interface.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface ILaunchPadAggregateRoot<TIdType> :
        ILaunchPadDomainEntity<TIdType>,
        ILaunchPadAggregateRootProperties<TIdType>, IAggregateRoot<TIdType>,
        IComparable<LaunchPadDomainEntityBase<TIdType>>, IEquatable<LaunchPadDomainEntityBase<TIdType>>
    {


    }
}
