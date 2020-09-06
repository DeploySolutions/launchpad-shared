﻿//LaunchPad Shared
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
    public interface IDomainEntity<TPrimaryKey> : ILaunchPadObject, IEntity<TPrimaryKey>,
        IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, ISoftDelete, IDeletionAudited, IPassivable
    {
        /// <summary>
        /// The Culture code of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        String Culture { get; set; }

        /// <summary>
        /// The id of the tenant that domain entity this belongs to (null if not known/applicable)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        TPrimaryKey TenantId { get; set; }

    }
}
