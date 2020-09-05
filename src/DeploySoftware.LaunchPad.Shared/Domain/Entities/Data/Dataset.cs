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
    
namespace DeploySoftware.LaunchPad.Shared.Domain.Data
{
    using System;

    public abstract class DataSet<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IDataSet<TPrimaryKey>
    {
        
        public int? TotalItemsCount { get; set; }
        
        public string Name { get; set; }

        public DataSet(
           string datasetName,
           string datasetDescription
        ) : base()
        {
            Name = datasetName;
            Metadata.DescriptionShort = datasetDescription;
            Metadata.DescriptionFull = datasetDescription;
            TotalItemsCount = 0;
        }
            
        protected DataSet() : base()
        {
            Name = String.Empty;
            TotalItemsCount = 0;
        }
            
    }

}
