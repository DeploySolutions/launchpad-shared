﻿//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Core.Domain
{
    using Abp.Domain.Entities;
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;

    public abstract class DataCatalogue<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IDataCatalogue<TPrimaryKey>
    {
        
        public int NumberofDatasets { get; set; }
        public int? TotalItemsCount { get; set; }
        

        public IEnumerable<IDataSet<TPrimaryKey>> DataSets { get; set; }
        public int TenantId { get;set; }

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
            NumberofDatasets = _numberOfDatasets;
            TotalItemsCount = _totalNumberOfRecords;
        }
            
        protected DataCatalogue(int tenantId) :base()
        {
            TenantId = tenantId;
            Name = String.Empty;
            NumberofDatasets = DataSets.Count();
            TotalItemsCount = 0;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DataCatalogue(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            NumberofDatasets = info.GetInt32("NumberofDatasets");
            TotalItemsCount = info.GetInt32("TotalItemsCount");
        }


        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("NumberofDatasets", NumberofDatasets);
            info.AddValue("TotalItemsCount", TotalItemsCount);
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
            sb.AppendFormat(" NumberofDatasets={0};", NumberofDatasets); 
            sb.AppendFormat(" TotalItemsCount={0};", TotalItemsCount);
            sb.Append(']');
            return sb.ToString();
        }

    }

}
