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

using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Domain
{
    /// <summary>
    /// Base class for application-specific information
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the key id field</typeparam>
    [Serializable()]
    public class LaunchPadApplication<TIdType> : DomainEntityBase<TIdType>, ILaunchPadApplication<TIdType>, IMayHaveTenant
    {

        /// <summary>
        /// The default culture of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IApplicationInformation<TIdType> Info
        {
            get; set;
        }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<IModule<TIdType>> Modules { get; set; }


        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<ITenantInformation<TIdType>> TenantInfo { get; set; }
        public int? TenantId { get; set; }


        #region "Constructors"

        public LaunchPadApplication(int? tenantId) : base()
        {
            TenantId = tenantId;
            Info = new ApplicationInformation<TIdType>(tenantId);
            TenantInfo = new List<ITenantInformation<TIdType>>();
            Modules = new List<IModule<TIdType>>();
        }

        public LaunchPadApplication(int? tenantId, TIdType id, string cultureName) : base(id, cultureName)
        {
            Info = new ApplicationInformation<TIdType>(tenantId);
            TenantInfo = new List<ITenantInformation<TIdType>>();
            Modules = new List<IModule<TIdType>>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadApplication(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            Info = (ApplicationInformation<TIdType>)info.GetValue("Info", typeof(ApplicationInformation<TIdType>));
            TenantInfo = (IEnumerable<ITenantInformation<TIdType>>)info.GetValue("TenantInfo", typeof(IEnumerable<ITenantInformation<TIdType>>)); 
            Modules = (IEnumerable<IModule<TIdType>>)info.GetValue("Modules", typeof(IEnumerable<IModule<TIdType>>));
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
            info.AddValue("Info", Info);
            info.AddValue("TenantInfo", TenantInfo); 
            info.AddValue("Modules", Modules);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Application : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" Info={0};", Info);
            sb.AppendFormat(" TenantInfo={0};", TenantInfo);
            sb.AppendFormat(" Modules={0};", Modules);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
