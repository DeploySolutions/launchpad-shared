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

namespace DeploySoftware.LaunchPad.Shared.Domain.Metadata
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// This interface represents a "tag" applied to an entity. Tags provide additional metadata information about
    /// an entity, and can be formal (as in some form of taxonomy) or informal ("Folksonomy").
    /// </summary>
    public interface ILaunchPadMetadataTag
    {
        /// <summary>
        /// The name of this metadata tag
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String Key { get; set; }

        /// <summary>
        /// The value of this metadata tag
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String Value { get; set; }

        /// <summary>
        /// The scheme of this metadata tag, if any
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String Schema { get; set; }

    }
}
