// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMustHaveCulture.cs" company="Deploy Software Solutions, inc.">
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




using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{

    /// <summary>
    /// Interface IMustHaveCulture
    /// </summary>
    public partial interface IMustHaveCulture
    {
        /// <summary>
        /// The culture of this entity
        /// </summary>
        /// <value>The culture (using the .NET culture values).</value>
        [DataObjectField(true)]
        [XmlAttribute]
        CultureInfo Culture { get; set; }

    }
}
