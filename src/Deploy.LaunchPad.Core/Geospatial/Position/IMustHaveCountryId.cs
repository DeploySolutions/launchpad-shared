// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMustHaveCountryId.cs" company="Deploy Software Solutions, inc.">
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

using Deploy.LaunchPad.Core.Domain;

namespace Deploy.LaunchPad.Core.Geospatial
{
    using Deploy.LaunchPad.Core.Domain.Model;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// This interface defines the details of the country in which this item is located, using a GUID ID value for the Countries, which may 
    /// be stored in the database.
    /// </summary>
    public partial interface IMustHaveCountryId : ILaunchPadObject
    {

        ///<summary>
        /// Country Id GUID
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public System.Guid CountryId { get; set; }


    }
}