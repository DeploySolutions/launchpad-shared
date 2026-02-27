// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="LaunchPadApplication.cs" company="Deploy Software Solutions, inc.">
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

using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Core.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;


namespace Deploy.LaunchPad.Core.Abp.SoftwareApplications
{
    /// <summary>
    /// Base class for application-specific information
    /// </summary>
    /// <typeparam name="TIdType">The type of the key id field</typeparam>
    /// <typeparam name="TEntityIdType">The base ID type of any domain entities contained within the application</typeparam>
    [Serializable()]
    public partial class LaunchPadApplication<TIdType, TEntityIdType> : Entity<TIdType>, ILaunchPadApplication<TIdType, TEntityIdType>, IMayHaveTenant
    {

        /// <summary>
        /// The default culture of this application
        /// </summary>
        /// <value>The application information.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ApplicationDetails<TIdType> AppInfo
        {
            get; set;
        }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        /// <value>The modules.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual List<Module<TIdType, TEntityIdType>> Modules { get; set; }


        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        /// <value>The tenant information.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual List<TenantDetails<TIdType>> TenantInfo { get; set; }
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual System.Guid? TenantId { get; set; }


        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadApplication{TIdType, TEntityIdType}"/> class.
        /// </summary>
        public LaunchPadApplication() : base()
        {
            AppInfo = new ApplicationDetails<TIdType>();
            TenantInfo = new List<TenantDetails<TIdType>>();
            Modules = new List<Module<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadApplication{TIdType, TEntityIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public LaunchPadApplication(System.Guid? tenantId) : base()
        {
            TenantId = tenantId;
            AppInfo = new ApplicationDetails<TIdType>();
            AppInfo.TenantId = tenantId;
            TenantInfo = new List<TenantDetails<TIdType>>();
            Modules = new List<Module<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadApplication{TIdType, TEntityIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        [SetsRequiredMembers]
        public LaunchPadApplication(System.Guid? tenantId, TIdType id, string cultureName) : base(id, cultureName)
        {
            TenantId = tenantId;
            AppInfo = new ApplicationDetails<TIdType>();
            AppInfo.TenantId = tenantId;
            TenantInfo = new List<TenantDetails<TIdType>>();
            Modules = new List<Module<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadApplication(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            AppInfo = (ApplicationDetails<TIdType>)info.GetValue("Info", typeof(ApplicationDetails<TIdType>));
            TenantInfo = (List<TenantDetails<TIdType>>)info.GetValue("TenantInfo", typeof(List<TenantDetails<TIdType>>));
            Modules = (List<Module<TIdType, TEntityIdType>>)info.GetValue("Modules", typeof(List<Module<TIdType, TEntityIdType>>));
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
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
