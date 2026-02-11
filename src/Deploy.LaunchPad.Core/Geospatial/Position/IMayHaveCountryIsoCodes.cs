// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMayHaveCountryIsoCodes.cs" company="Deploy Software Solutions, inc.">
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

using Deploy.LaunchPad.Domain;

namespace Deploy.LaunchPad.Domain.Geospatial
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Util.Elements;

    /// <summary>
    /// This interface defines the details of the country in which this item is located, using the ISO Alpha-3 code.
    /// </summary>
    public partial interface IMayHaveCountryIsoCodes : ILaunchPadObject
    {

        ///<summary>
        /// ISO Code alpha2
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(2)]
        public string? IsoCodeAlpha2 { get; set; }

        ///<summary>
        /// ISO Code alpha3
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(3)]
        public string? IsoCodeAlpha3 { get; set; }


    }
}