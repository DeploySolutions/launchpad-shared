// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IDataSet.cs" company="Deploy Software Solutions, inc.">
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

using Deploy.LaunchPad.Core.Licenses;
using Deploy.LaunchPad.Core.Metadata;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Data
{

    /// <summary>
    /// Interface IDataSet
    /// </summary>
    public partial interface ILaunchPadDataSet<TDictionaryKey, TSchemaFormat> : IMayHaveSchemaDetails<TSchemaFormat>
        where TDictionaryKey : struct
    {
        
        public string Contact { get; set; }

        public string Quality { get; set; }

        public string Format { get; set; }
        public string AccessRights { get; set; }
        public string UsageNotes { get; set; }

        public License License { get; set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public long Count { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public IDictionary<TDictionaryKey, ILaunchPadDataPoint> Data { get; }

        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="dataPoint">The data point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddData(TDictionaryKey key, ILaunchPadDataPoint dataPoint);


    }
}
