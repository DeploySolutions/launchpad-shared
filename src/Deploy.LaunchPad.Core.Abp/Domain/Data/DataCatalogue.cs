﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="DataCatalogue.cs" company="Deploy Software Solutions, inc.">
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


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Domain.Model;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Class DataCatalogue.
    /// Implements the <see cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Domain.IDataCatalogue{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <seealso cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Domain.IDataCatalogue{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    public abstract class DataCatalogue<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey> :
        LaunchPadDomainEntityBase<TPrimaryKey>,
        IDataCatalogue<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {

        /// <summary>
        /// Gets or sets the items count.
        /// </summary>
        /// <value>The items count.</value>
        public long ItemsCount { get; set; }


        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public int? TenantId { get; set; }
        /// <summary>
        /// Gets or sets the data sets.
        /// </summary>
        /// <value>The data sets.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<IDataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>> DataSets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        /// <summary>
        /// Gets or sets the data sets count.
        /// </summary>
        /// <value>The data sets count.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public int DataSetsCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCatalogue{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        public DataCatalogue() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCatalogue{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="_datacatalogueName">Name of the datacatalogue.</param>
        /// <param name="_datacatalogueDescription">The datacatalogue description.</param>
        /// <param name="_numberOfDatasets">The number of datasets.</param>
        /// <param name="_totalNumberOfRecords">The total number of records.</param>
        public DataCatalogue(
            int tenantId,
            string _datacatalogueName,
            string _datacatalogueDescription,
            int _numberOfDatasets,
            int _totalNumberOfRecords
            ) : base()
        {
            TenantId = tenantId;
            Name = new EntityName(_datacatalogueName);
            Description = new EntityDescription(_datacatalogueDescription);
            DataSetsCount = _numberOfDatasets;
            ItemsCount = _totalNumberOfRecords;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCatalogue{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected DataCatalogue(int tenantId) : base()
        {
            TenantId = tenantId;
            Name = new EntityName(String.Empty);
            DataSetsCount = 0;
            ItemsCount = 0;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DataCatalogue(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DataSetsCount = info.GetInt32("DataSetsCount");
            ItemsCount = info.GetInt32("ItemsCount");
        }


        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("DataSetsCount", DataSetsCount);
            info.AddValue("ItemsCount", ItemsCount);
        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DataCatalogue : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" DataSetsCount={0};", DataSetsCount);
            sb.AppendFormat(" ItemsCount={0};", ItemsCount);
            sb.Append(']');
            return sb.ToString();
        }

    }

}
