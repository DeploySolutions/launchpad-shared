// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IDataPoint.cs" company="Deploy Software Solutions, inc.">
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
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{

    /// <summary>
    /// Interface for determining the order in which something will be displayed in a list.
    /// The lower the number, the earlier it will be displayed.  
    /// This is used in various places to determine the order of display of items in a list.
    /// </summary>
    public partial interface IMayHaveSequenceNumber
    {
        /// <summary>
        /// Specifies an order number for this item, if any, for sorting and ordering purposes.  
        /// The lower the number, the earlier it will be displayed in a list.
        /// </summary>
        /// <value>The sequence number, if any.</value>
        [DataObjectField(false)]
        [XmlElement]
        public int? SeqNum { get; set; }
    }
}
