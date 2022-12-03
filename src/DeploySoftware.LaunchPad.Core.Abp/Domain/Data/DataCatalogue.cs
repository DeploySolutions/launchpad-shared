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


using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Abp.Domain
{

    public abstract class DataCatalogue<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey> : 
        DomainEntityBase<TPrimaryKey>, 
        IDataCatalogue<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {
        
        public long ItemsCount { get; set; }
        

        public int? TenantId { get;set; }
        public IEnumerable<IDataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>> DataSets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int DataSetsCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DataCatalogue() : base()
        {

        }

        public DataCatalogue(
            int tenantId,
            string _datacatalogueName,
            string _datacatalogueDescription,
            int _numberOfDatasets, 
            int _totalNumberOfRecords
            ) : base()
        {
            TenantId = tenantId;
            Name = _datacatalogueName;
            DescriptionShort = _datacatalogueDescription; 
            DescriptionFull = _datacatalogueDescription;
            DataSetsCount = _numberOfDatasets;
            ItemsCount = _totalNumberOfRecords;
        }
            
        protected DataCatalogue(int tenantId) :base()
        {
            TenantId = tenantId;
            Name = String.Empty;
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
        /// <param name="info"></param>
        /// <param name="context"></param>
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
