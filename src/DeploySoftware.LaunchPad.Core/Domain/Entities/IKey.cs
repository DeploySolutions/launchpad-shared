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

namespace DeploySoftware.LaunchPad.Core.Domain
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Any object in the platform can be uniquely identified by its DomainEntityKey. 
    /// The key is composite and contains two required properties - its Id field, of a generic type, and
    /// a culture code. This allows us to match related content in different cultures.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id field (often a GUID)</typeparam>
    public interface IKey<TIdType> : ISerializable
    {
        /// <summary>
        /// The globally unique identifier that identifies this object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        TIdType LaunchPadId { get; set; }

        /// <summary>
        /// The Culture code of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        String Culture { get; set; }
    }
}
