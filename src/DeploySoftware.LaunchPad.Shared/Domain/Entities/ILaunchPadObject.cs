﻿//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 

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
    using System.Runtime.Serialization;
    using System.ComponentModel;

    /// <summary>
    /// Every entity or file managed by the platform must implement this interface.    
    /// </summary>
    public interface ILaunchPadObject : ISerializable
    {

        /// <summary>
        /// Each Entity or File object can have an open-ended set of metadata applied to it, that helps to describe it.
        /// </summary>
        [DataObjectField(false)]
        MetadataInformation Metadata { get; set; }        

    }
}