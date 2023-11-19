// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="FilterOutput.cs" company="Deploy Software Solutions, inc.">
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


using Deploy.LaunchPad.Core.Abp.Domain.Model;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    /// <summary>
    /// Represents the minimal properties that may be used in order to filter an entity in a list
    /// </summary>
    /// <typeparam name="TEntityType">The type of the t entity type.</typeparam>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial class FilterOutput<TEntityType, TIdType>
        where TEntityType : LaunchPadDomainEntityBase<TIdType>
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public IEnumerable<TEntityType> Filter { get; set; }
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public FilterOutput() : base()
        {

        }

        #endregion


    }
}
