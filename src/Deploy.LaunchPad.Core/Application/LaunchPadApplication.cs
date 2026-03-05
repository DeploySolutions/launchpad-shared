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
using Deploy.LaunchPad.Util.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;


namespace Deploy.LaunchPad.Core.Application
{
    /// <summary>
    /// Base class for application-specific information
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the key id field</typeparam>
    /// <typeparam name="TEntityIdType">The base ID type of any domain entities contained within the application</typeparam>
    [Serializable()]
    public abstract partial class LaunchPadApplicationBase : DomainEntityBase<Guid>, 
        ILaunchPadApplication
    {

        /// <summary>
        /// The default culture of this application
        /// </summary>
        /// <value>The application information.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public required virtual ApplicationDetails AppInfo
        {
            get; set;
        }

        /// <summary>
        /// Each application can have an open-ended set of modules within that provide the functionality
        /// </summary>
        /// <value>The tenant information.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual List<TenantDetails> TenantInfo { get; set; }
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual System.Guid? TenantId { get; set; }

        /// <summary>
        /// Each application can have an open-ended set of components within that provide the functionality
        /// </summary>
        /// <value>The dictionary of components.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IDictionary<Guid, ILaunchPadComponent> Components { get; set; }

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadApplication{TPrimaryKey, TEntityIdType}"/> class.
        /// </summary>
        protected LaunchPadApplicationBase() : base()
        {
            AppInfo = new ApplicationDetails();
            TenantInfo = new List<TenantDetails>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadApplication{TPrimaryKey, TEntityIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        [SetsRequiredMembers]
        public LaunchPadApplicationBase(Guid id, string cultureName) : base(id, cultureName)
        {
            AppInfo = new ApplicationDetails();
            TenantInfo = new List<TenantDetails>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadApplicationBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            AppInfo = (ApplicationDetails)info.GetValue("Info", typeof(ApplicationDetails));
            TenantInfo = (List<TenantDetails>)info.GetValue("TenantInfo", typeof(List<TenantDetails>));
            Components = (IDictionary<Guid, ILaunchPadComponent>)info.GetValue("Components", typeof(Dictionary<Guid, ILaunchPadComponent>));
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
            info.AddValue("Components", Components);
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
            sb.Append(']');
            return sb.ToString();
        }
    }
}
