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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// This interface holds common metadata information for entities used by the framework,
    /// such as the author or the date last modified. It is a core component of any Entity class.
    /// </summary>
    public interface IMetadataInformation : IEquatable<MetadataInformation>
    {

        /// <summary>
        /// The author of this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Int64? CreatorId { get; set; }

        /// <summary>
        /// A full description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String DescriptionFull { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String DescriptionShort { get; set; }

        /// <summary>
        /// The display name that can be displayed as a label externally to users when referring to this object
        /// (rather than using a GUID, which is unfriendly but unique)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String DisplayName { get; set; }

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime DateCreated { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? DateLastModified { get; set; }

        /// <summary>
        /// The id of the person who last modified this object.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Int64? LastModifiedById { get; set; }

        /// <summary>
        /// Each entity in the framework can have a MIME type which is used to help display
        /// its information to Http-capable browsers. 
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String MimeType { get; set; }


    }
}
