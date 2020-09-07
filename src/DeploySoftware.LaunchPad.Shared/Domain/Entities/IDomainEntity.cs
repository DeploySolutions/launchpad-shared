//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Xml.Serialization;

    /// <summary>
    /// Marks any object as a Domain Entity that can be manipulated by the LaunchPad platform.
    /// Each entity is uniquely identified by its DomainEntityKey, and contains a 
    /// set of <see cref="MetadataInformation">MetadataInformation</see>.
    /// Each entity also implements ASP.NET Boilerplate's IEntity interface.
    /// </summary>
    public interface IDomainEntity<TIdType> : 
        ILaunchPadObject, IEntity<TIdType>,
        IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, IDeletionAudited,
        ISoftDelete, IPassivable, IMayHaveTenant,
        IComparable<DomainEntityBase<TIdType>>, IEquatable<DomainEntityBase<TIdType>>
    {

        /// <summary>
        /// The key (culture and Id) that uniquely identifies this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        DomainEntityKey<TIdType> Key { get; set; }


    }
}
