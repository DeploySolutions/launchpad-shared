//LaunchPad Shared
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Shared.Domain.Data
{
    using System;
    using Abp.Timing;

    public abstract class DataCatalogue<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IDataCatalogue<TPrimaryKey>
    {
        
        public int NumberofDatasets { get; set; }
        public int? TotalCount { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }
        
        public DateTime? LastModificationTime { get; set; }

        public IEnumerable<IDataSet<TPrimaryKey>> DataSets { get; set; } 

        public DataCatalogue(
            string _datacatalogueName,
            string _datacatalogueDescription,
            int _numberOfDatasets, 
            int _totalNumberOfRecords,
            DateTime _lastModificationTime
            ) : base()
        {
            Name = _datacatalogueName;
            Description = _datacatalogueDescription;
            NumberofDatasets = _numberOfDatasets;
            TotalCount = _totalNumberOfRecords;
            CreationTime = Clock.Now;
            LastModificationTime = _lastModificationTime;
        }
            
        protected DataCatalogue() :base()
        {
            Name = String.Empty;
            Description = String.Empty;
            CreationTime = DateTime.Now;
            LastModificationTime = DateTime.Now;
            NumberofDatasets = DataSets.Count();
            TotalCount = 0;
        }
            
    }

}
