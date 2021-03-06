﻿//LaunchPad Shared
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
    /// <typeparam name="TIdType">The type of the key id field</typeparam>
    /// <typeparam name="TEntityIdType">The base ID type of any domain entities contained within the application</typeparam>
    [Serializable()]
    public class LaunchPadApplication<TIdType,TEntityIdType> : DomainEntityBase<TIdType>, ILaunchPadApplication<TIdType, TEntityIdType>, IMayHaveTenant
    {

        /// <summary>
        /// The default culture of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ApplicationInformation<TIdType> AppInfo
        {
            get; set;
        }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual List<Module<TIdType, TEntityIdType>> Modules { get; set; }


        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual List<TenantInformation<TIdType>> TenantInfo { get; set; }
        public virtual int? TenantId { get; set; }


        #region "Constructors"

        public LaunchPadApplication() : base()
        {
            AppInfo = new ApplicationInformation<TIdType>();
            TenantInfo = new List<TenantInformation<TIdType>>();
            Modules = new List<Module<TIdType, TEntityIdType>>();
        }

        public LaunchPadApplication(int? tenantId) : base()
        {
            TenantId = tenantId;
            AppInfo = new ApplicationInformation<TIdType>(tenantId);
            TenantInfo = new List<TenantInformation<TIdType>>();
            Modules = new List<Module<TIdType, TEntityIdType>>();
        }

        public LaunchPadApplication(int? tenantId, TIdType id, string cultureName) : base(id, cultureName)
        {
            AppInfo = new ApplicationInformation<TIdType>(tenantId);
            TenantInfo = new List<TenantInformation<TIdType>>();
            Modules = new List<Module<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadApplication(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            AppInfo = (ApplicationInformation<TIdType>)info.GetValue("Info", typeof(ApplicationInformation<TIdType>));
            TenantInfo = (List<TenantInformation<TIdType>>)info.GetValue("TenantInfo", typeof(List<TenantInformation<TIdType>>)); 
            Modules = (List<Module<TIdType, TEntityIdType>>)info.GetValue("Modules", typeof(List<Module<TIdType, TEntityIdType>>));
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Info", AppInfo);
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
            sb.AppendFormat(" Info={0};", AppInfo);
            sb.AppendFormat(" TenantInfo={0};", TenantInfo);
            sb.AppendFormat(" Modules={0};", Modules);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
