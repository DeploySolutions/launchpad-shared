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

namespace DeploySoftware.LaunchPad.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// The storage location of the file bytes.
    /// </summary>
    public interface IFileStorageLocation : ILaunchPadObject,
        IComparable<GenericFileStorageLocation>, IEquatable<GenericFileStorageLocation>
    {
        [DataObjectField(true)]
        [XmlAttribute]
        public string Id { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// A short description for this storage location
        /// </summary>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this storage location
        /// </summary>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public string? DescriptionFull { get; set; }

        [DataObjectField(true)]
        [XmlAttribute]
        public bool IsReadOnly { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public Uri RootUri { get; set; }


        [DataObjectField(false)]
        [XmlAttribute]
        public string DefaultPrefix { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public FileStorageProviderTypeEnum Provider { get; set; }

        /// <summary>
        /// The location have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<MetadataTag> Tags { get; set; }
    }
}
