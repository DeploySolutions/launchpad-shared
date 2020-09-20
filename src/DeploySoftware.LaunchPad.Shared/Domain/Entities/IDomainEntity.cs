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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
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
        ISoftDelete, IPassivable,
        IComparable<DomainEntityBase<TIdType>>, IEquatable<DomainEntityBase<TIdType>>
    {

        /// <summary>
        /// The culture of this entity
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [Key]
        String Culture { get; set; }

        /// <summary>
        /// The display name of this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String DisplayName { get; set; }

        /// <summary>
        /// A short description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        String DescriptionFull { get; set; }

        /// <summary>
        /// The key (culture and Id) that uniquely identifies this object
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        DomainEntityKey<TIdType> Key { get; set; }

        /// <summary>
        /// Each entity can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        IEnumerable<MetadataTag> Tags { get; set; }

    }
}
