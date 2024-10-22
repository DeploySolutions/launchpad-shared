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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Domain;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Interface IDataSet
    /// Extends the <see cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    /// Extends the <see cref="IMayHaveTenant" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <seealso cref="ILaunchPadDomainEntity{TPrimaryKey}" />
    /// <seealso cref="IMayHaveTenant" />
    public partial interface IDataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey> : ILaunchPadDomainEntity<TPrimaryKey>, IMayHaveTenant
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {
        /// <summary>
        /// Describes the schema (where known) according to which all of the data in this set is structured.
        /// </summary>
        /// <value>The schema.</value>
        public ISchemaDetails Schema { get; set; }

        public string Contact { get; set; }

        public string Quality { get; set; }

        public string Format { get; set; }
        public string AccessRights { get; set; }
        public string UsageNotes { get; set; }

        public License License { get; set; }

        public IList<DataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>> Related { get; set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public long Count { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public IDictionary<TDictionaryKey, IDataPoint<TDataPointPrimaryKey>> Data { get; }

        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="dataPoint">The data point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddData(TDictionaryKey key, IDataPoint<TDataPointPrimaryKey> dataPoint);


    }
}
