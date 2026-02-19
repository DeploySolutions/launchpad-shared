// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IDeploymentProcess.cs" company="Deploy Software Solutions, inc.">
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

using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.Core.Abp.Deployments
{

    /// <summary>
    /// Represents the process which a deployment will follow as it takes a release candidate (set of code, data, and resources) and places it in a destination environment.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    public partial interface IDeploymentProcess<TPrimaryKey> : ILaunchPadDomainEntity<TPrimaryKey>
    {

        /// <summary>
        /// The URI to the deployment documentation
        /// </summary>
        /// <value>The documentation URI.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri DocumentationUri { get; set; }

        /// <summary>
        /// The URI to the diagram
        /// </summary>
        /// <value>The diagram URI.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri DiagramUri { get; set; }

    }
}
