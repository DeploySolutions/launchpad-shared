// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IMetadataInformation.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Core.Metadata
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// This interface holds common metadata information for entities used by the framework,
    /// such as the author or the date last modified. It is a core component of any Entity class.
    /// </summary>
    public partial interface IMetadataInformation : IEquatable<MetadataInformation>
    {

   
        /// <summary>
        /// The id of the tenant that domain entity this belongs to (null if not known/applicable)
        /// </summary>
        /// <value>The tenant identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        int? TenantId { get; set; }

        /// <summary>
        /// The display name that can be displayed as a label externally to users when referring to this object
        /// (rather than using a GUID, which is unfriendly but unique)
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String Name { get; set; }

        /// <summary>
        /// A full description of this item.
        /// </summary>
        /// <value>The description full.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String DescriptionFull { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String DescriptionShort { get; set; }


        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime CreationTime { get; set; }

        /// <summary>
        /// The author of this entity
        /// </summary>
        /// <value>The creator user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        Int64? CreatorUserId { get; set; }

        /// <summary>
        /// The date and time that this object was last modified.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// The id of the person who last modified this object.
        /// </summary>
        /// <value>The last modifier user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        Int64? LastModifierUserId { get; set; }

        /// <summary>
        /// The deleter of this entity
        /// </summary>
        /// <value>The deleter user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        Int64? DeleterUserId { get; set; }

        /// <summary>
        /// The date and time that this object were deleted.
        /// </summary>
        /// <value>The deletion time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? DeletionTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        bool IsDeleted { get; set; }

    }
}
