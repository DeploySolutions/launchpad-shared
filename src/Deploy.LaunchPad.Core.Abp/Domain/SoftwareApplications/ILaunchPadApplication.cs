﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ILaunchPadApplication.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Core.Abp.Domain.Model;

    /// <summary>
    /// Represents an application in the LaunchPad RAD framework.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TEntityIdType">The type of the t entity identifier type.</typeparam>
    public partial interface ILaunchPadApplication<TIdType, TEntityIdType> : ILaunchPadDomainEntity<TIdType>
    {

        /// <summary>
        /// The default culture of this application
        /// </summary>
        /// <value>The application information.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        ApplicationDetails<TIdType> AppInfo
        {
            get; set;
        }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        /// <value>The tenant information.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        List<TenantDetails<TIdType>> TenantInfo { get; set; }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        /// <value>The modules.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        List<Module<TIdType, TEntityIdType>> Modules { get; set; }

    }
}
