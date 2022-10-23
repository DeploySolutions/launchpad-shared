﻿//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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


namespace DeploySoftware.LaunchPad.Core.Abp.Domain.SoftwareApplications
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a comopnent in a software module.
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    public interface IComponent<TIdType, TEntityIdType> : IDomainEntity<TIdType>
    {
        /// <summary>
        /// Each component can have 0 to many domain entities
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        IList<DomainEntityBase<TEntityIdType>> DomainEntities { get; set; }

    }
}