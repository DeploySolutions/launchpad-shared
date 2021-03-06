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
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents an application in the LaunchPad RAD framework.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the id</typeparam>
    public interface ILaunchPadApplication<TIdType, TEntityIdType> : IDomainEntity<TIdType>
    {

        /// <summary>
        /// The default culture of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        ApplicationInformation<TIdType> AppInfo
        {
            get; set;
        }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        List<TenantInformation<TIdType>> TenantInfo { get; set; }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        List<Module<TIdType, TEntityIdType>> Modules { get; set; }

    }
}
