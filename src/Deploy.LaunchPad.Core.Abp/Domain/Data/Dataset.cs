// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Dataset.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
    using Deploy.LaunchPad.Core.Abp.Domain.Model;
    using Deploy.LaunchPad.Core.Domain.Model;

    /// <summary>
    /// Class DataSet.
    /// Implements the <see cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Domain.IDataSet{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <seealso cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Domain.IDataSet{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    public abstract class DataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey> : LaunchPadDomainEntityBase<TPrimaryKey>, IDataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public virtual long Count
        {
            get
            {
                return _data.Count;
            }
        }

        /// <summary>
        /// The data
        /// </summary>
        protected IDictionary<TDictionaryKey, IDataPoint<TDataPointPrimaryKey>> _data;
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public IDictionary<TDictionaryKey, IDataPoint<TDataPointPrimaryKey>> Data
        {
            get { return _data; }
        }

        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual int? TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Describes the schema (where known) according to which this data is structured.
        /// </summary>
        /// <value>The schema.</value>
        public ISchemaDetails Schema { get; set; }

        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="dataPoint">The data point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool AddData(TDictionaryKey key, IDataPoint<TDataPointPrimaryKey> dataPoint)
        {
            if (dataPoint != null)
            {
                // add the data point
                bool wasAdded = _data.TryAdd(key, dataPoint);

            }
            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSet{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        public DataSet() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSet{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="datasetDescription">The dataset description.</param>
        public DataSet(
            int tenantId,
           string datasetName,
           string datasetDescription
        ) : base()
        {

            Name = new ElementName(datasetName);
            Description = new ElementDescription(datasetDescription);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSet{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected DataSet(int tenantId) : base()
        {
            TenantId = tenantId;
            Name = new ElementName(String.Empty);
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DataSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _data = (IDictionary<TDictionaryKey, IDataPoint<TDataPointPrimaryKey>>)info.GetValue("Data", typeof(IDictionary<TDictionaryKey, IDataPoint<TDataPointPrimaryKey>>));
            Schema = (ISchemaDetails)info.GetValue("Schema", typeof(ISchemaDetails));
        }


        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Count", Count);
            info.AddValue("Data", Data);
            info.AddValue("Schema", Schema);
        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Dataset : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" Schema={0};", Schema);
            sb.AppendFormat(" Count={0};", Count);
            sb.Append(']');
            return sb.ToString();
        }
    }

}
