//LaunchPad Shared
// Copyright (c) 2018-2022 Deploy Software Solutions, inc. 

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

    public abstract class DataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey> : DomainEntityBase<TPrimaryKey>, IDataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {
        public virtual long Count
        {
            get
            {
                return _data.Count;
            }
        }

        protected IDictionary<TDictionaryKey, IDataPoint<TDataPointPrimaryKey>> _data;
        public IDictionary<TDictionaryKey, IDataPoint<TDataPointPrimaryKey>> Data
        {
            get { return _data; }
        }

        public virtual int? TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Describes the schema (where known) according to which this data is structured.
        /// </summary>
        public ISchemaDetails Schema { get; set; }

        public virtual bool AddData(TDictionaryKey key, IDataPoint<TDataPointPrimaryKey> dataPoint)
        {
            if (dataPoint != null)
            {
                // add the data point
                bool wasAdded = _data.TryAdd(key, dataPoint);

            }
            return true;
        }

        public DataSet() : base()
        {
        }

        public DataSet(
            int tenantId,
           string datasetName,
           string datasetDescription
        ) : base()
        {
            Name = datasetName;
            DescriptionShort = datasetDescription;
            DescriptionFull = datasetDescription;
        }

        protected DataSet(int tenantId) : base()
        {
            TenantId = tenantId;
            Name = String.Empty;
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
        /// <param name="info"></param>
        /// <param name="context"></param>
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
