// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IDataCatalog.cs" company="Deploy Software Solutions, inc.">
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


using Deploy.LaunchPad.Util.Metadata;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Domain.Data
{
    /// <summary>
    /// Interface IDataCatalog
    /// </summary>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    public partial interface ILaunchPadDataCatalog<TDictionaryKey, TSchemaFormat> : IMayHaveSchema<TSchemaFormat>
        where TDictionaryKey : struct
    {

        /// <summary>
        /// Gets or sets the data sets count.
        /// </summary>
        /// <value>The data sets count.</value>
        int DataSetsCount { get; }

        /// <summary>
        /// Gets or sets the items count.
        /// </summary>
        /// <value>The items count.</value>
        long ItemsCount { get;  }


        /// <summary>
        /// Gets or sets the data sets.
        /// </summary>
        /// <value>The data sets.</value>
        IEnumerable<ILaunchPadDataSet<TDictionaryKey, TSchemaFormat>> DataSets { get; set; }

    }
}
