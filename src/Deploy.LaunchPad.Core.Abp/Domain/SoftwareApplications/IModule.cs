﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IModule.cs" company="Deploy Software Solutions, inc.">
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


namespace Deploy.LaunchPad.Core.Abp.Domain.SoftwareApplications
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Core.Abp.Domain.Model;

    /// <summary>
    /// Represents a module in an application.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TEntityIdType">The type of the t entity identifier type.</typeparam>
    public partial interface IModule<TIdType, TEntityIdType> : ILaunchPadDomainEntity<TIdType>
    {

        /// <summary>
        /// The type of the module
        /// </summary>
        /// <value>The type.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String Type
        {
            get; set;
        }

        /// <summary>
        /// The default culture of this tenant
        /// </summary>
        /// <value>The culture default.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String CultureDefault
        {
            get; set;
        }

        /// <summary>
        /// Each module can have an open-ended set of components within that provide the functionality
        /// </summary>
        /// <value>The components.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        IList<Component<TIdType, TEntityIdType>> Components { get; set; }

    }
}
