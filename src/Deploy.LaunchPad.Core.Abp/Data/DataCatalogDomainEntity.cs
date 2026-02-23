// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="DataCatalogue.cs" company="Deploy Software Solutions, inc.">
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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Deploy.LaunchPad.Data;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Files;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.Core.Abp.Data
{

    /// <summary>
    /// Class DataCatalogue.
    /// Implements the <see cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// Implements the <see cref="Abp.Domain.IDataCatalogDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <seealso cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// <seealso cref="Abp.Domain.IDataCatalogDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    public abstract partial class DataCatalogDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat> :
        LaunchPadDomainEntityBase<TPrimaryKey>,
        IDataCatalogDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {
        /// <summary>
        /// Describes the schema (where known) according to which this data is structured.
        /// </summary>
        /// <value>The schema.</value>
        public virtual ILaunchPadSchemaDetails<TSchemaFormat>? Schema { get; set; }

        /// <summary>
        /// Gets or sets the items count.
        /// </summary>
        /// <value>The items count.</value>
        public virtual long ItemsCount { get; set; }


        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }
        /// <summary>
        /// Gets or sets the data sets.
        /// </summary>
        /// <value>The data sets.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<ILaunchPadDataSet<TDictionaryKey, TSchemaFormat>> DataSets { get; set; }

        /// <summary>
        /// Gets or sets the data sets count.
        /// </summary>
        /// <value>The data sets count.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual int DataSetsCount
        {
            get { return DataSets.Count(); }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DataCatalogDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        public DataCatalogDomainEntity() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCatalogDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="_datacatalogueName">Name of the datacatalogue.</param>
        /// <param name="_datacatalogueDescription">The datacatalogue description.</param>
        /// <param name="_numberOfDatasets">The number of datasets.</param>
        /// <param name="_totalNumberOfRecords">The total number of records.</param>
        public DataCatalogDomainEntity(
            int tenantId,
            string _datacatalogueName,
            string _datacatalogueDescription,
            int _totalNumberOfRecords
            ) : base()
        {
            TenantId = tenantId;
            Name = new ElementName(_datacatalogueName);
            Description = new ElementDescription(_datacatalogueDescription);
            ItemsCount = _totalNumberOfRecords;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCatalogDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected DataCatalogDomainEntity(int tenantId) : base()
        {
            TenantId = tenantId;
            Name = new ElementName(String.Empty);
            ItemsCount = 0;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DataCatalogDomainEntity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
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
