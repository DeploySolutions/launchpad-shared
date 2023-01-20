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

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Represents the process which a deployment will follow as it takes a release candidate (set of code, data, and resources) and places it in a destination environment.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public interface IDeploymentProcess<TPrimaryKey> : IDomainEntity<TPrimaryKey>
    {

        /// <summary>
        /// The URI to the deployment documentation
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri DocumentationUri { get; set; }

        /// <summary>
        /// The URI to the diagram
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri DiagramUri { get; set; }

    }
}