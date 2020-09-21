//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Shared.Domain
{
    /// <summary>
    /// Base class for application-specific information
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the key id field</typeparam>
    [Serializable()]
    public class Module<TIdType> : DomainEntityBase<TIdType>, IModule<TIdType>
    {
        /// <summary>
        /// The default culture of this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String CultureDefault
        {
            get; set;
        }

        /// <summary>
        /// Each module can have an open-ended set of components within that provide the functionality
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<MetadataTag> Components { get; set; }

        #region "Constructors"

        public Module(int? tenantId) : base(tenantId)
        {
            Components = new List<MetadataTag>();
        }

        public Module(int? tenantId, TIdType id, string cultureName) : base(tenantId, id, cultureName)
        {
            CultureDefault = cultureName;
            Components = new List<MetadataTag>();
        }

        public Module(int? tenantId, TIdType id, string cultureName, String cultureDefault) : base(tenantId, id, cultureName)
        {
            CultureDefault = cultureDefault;
            Components = new List<MetadataTag>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected Module(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            CultureDefault = info.GetString("CultureDefault");
            Components = (IEnumerable<MetadataTag>)info.GetValue("Components", typeof(IEnumerable<MetadataTag>));
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CultureDefault", CultureDefault);
            info.AddValue("Components", Components);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Module : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" CultureDefault={0};", CultureDefault);
            sb.AppendFormat(" Components={0};", Components);            
            sb.Append("]");
            return sb.ToString();
        }
    }
}
