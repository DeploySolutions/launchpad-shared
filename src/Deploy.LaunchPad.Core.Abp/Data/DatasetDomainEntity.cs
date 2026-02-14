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

namespace Deploy.LaunchPad.Core.Abp.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
    using Deploy.LaunchPad.Core.Abp.Model;
    using Deploy.LaunchPad.Util.Data;
    using Deploy.LaunchPad.Domain.Model;
    using Deploy.LaunchPad.Util.Licenses;
    using Deploy.LaunchPad.Util;
    using Deploy.LaunchPad.Files;
    using Deploy.LaunchPad.Util.Metadata;


    /// <summary>
    /// Class DataSet.
    /// Implements the <see cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// Implements the <see cref="Abp.Domain.IDataSetDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <typeparam name="TSchemaFormat">The type of the schema (ex a Json or XSD related generic type>.</typeparam>
    /// <seealso cref="LaunchPadDomainEntityBase{TPrimaryKey}" />
    /// <seealso cref="Abp.Domain.IDataSetDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    public abstract partial class DatasetDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat> : LaunchPadDomainEntityBase<TPrimaryKey>, IDataSetDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {
        public virtual string Contact { get; set; }

        public virtual string Quality { get; set; }

        public virtual string Format { get; set; }
        public virtual string AccessRights { get; set; }
        public virtual string UsageNotes { get; set; }

        public virtual License License  { get; set; }

        public virtual IList<DatasetDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat>> Related { get; set; }

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
        protected IDictionary<TDictionaryKey, ILaunchPadDataPoint> _data;
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public virtual IDictionary<TDictionaryKey, ILaunchPadDataPoint> Data
        {
            get { return _data; }
        }

        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Describes the schema (where known) according to which this data is structured.
        /// </summary>
        /// <value>The schema.</value>
        public virtual ILaunchPadSchemaDetails<TSchemaFormat>? Schema { get; set; }

        IDictionary<TDictionaryKey, ILaunchPadDataPoint> ILaunchPadDataSet<TDictionaryKey, TSchemaFormat>.Data => throw new NotImplementedException();

        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="dataPoint">The data point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool AddData(TDictionaryKey key, ILaunchPadDataPoint dataPoint)
        {
            if (dataPoint != null)
            {
                // add the data point
                bool wasAdded = _data.TryAdd(key, dataPoint);

            }
            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        public DatasetDomainEntity() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="datasetDescription">The dataset description.</param>
        public DatasetDomainEntity(
            int tenantId,
           string datasetName,
           string datasetDescription
        ) : base()
        {

            Name = new ElementName(datasetName);
            Description = new ElementDescription(datasetDescription);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected DatasetDomainEntity(int tenantId) : base()
        {
            TenantId = tenantId;
            Name = new ElementName(String.Empty);
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DatasetDomainEntity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _data = (IDictionary<TDictionaryKey, ILaunchPadDataPoint>)info.GetValue("Data", typeof(IDictionary<TDictionaryKey, ILaunchPadDataPoint>));
            Schema = (ILaunchPadSchemaDetails<TSchemaFormat>)info.GetValue("Schema", typeof(ILaunchPadSchemaDetails<TSchemaFormat>));
            Contact = info.GetString("Contact");
            Quality = info.GetString("Quality");
            AccessRights = info.GetString("AccessRights");
            Format = info.GetString("Format");
            UsageNotes = info.GetString("UsageNotes"); 
            License = (License)info.GetValue("Data", typeof(License));

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
            info.AddValue("Contact", Contact);
            info.AddValue("Quality", Quality);
            info.AddValue("AccessRights", AccessRights);
            info.AddValue("Format", Format);
            info.AddValue("UsageNotes", UsageNotes);
            info.AddValue("Related", Related);
            info.AddValue("License", License);
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
            sb.AppendFormat(" Contact={0};", Contact);
            sb.AppendFormat(" Quality={0};", Quality);
            sb.AppendFormat(" AccessRights={0};", AccessRights);
            sb.AppendFormat(" Format={0};", Format);
            sb.AppendFormat(" UsageNotes={0};", UsageNotes);
            sb.AppendFormat(" Related={0};", Related);
            sb.AppendFormat(" License={0};", License);
            sb.Append(']');
            return sb.ToString();
        }

    }

}
