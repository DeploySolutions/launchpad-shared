//LaunchPad Shared
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
    
namespace DeploySoftware.LaunchPad.Shared.Domain
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;

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


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DataSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            TotalItemsCount = info.GetInt32("TotalItemsCount");
        }


        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);         
            info.AddValue("TotalItemsCount", TotalItemsCount);
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
            sb.AppendFormat(" TotalItemsCount={0};", TotalItemsCount);
            sb.Append("]");
            return sb.ToString();
        }

    }

}
