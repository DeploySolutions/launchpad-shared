// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Module.cs" company="Deploy Software Solutions, inc.">
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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;


namespace Deploy.LaunchPad.Core.Abp.SoftwareApplications
{
    /// <summary>
    /// Base class for application-specific information
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TEntityIdType">The type of the t entity identifier type.</typeparam>
    [Serializable()]
    public partial class Module<TIdType, TEntityIdType> : LaunchPadDomainEntityBase<TIdType>, IModule<TIdType, TEntityIdType>, IMayHaveTenant
    {
        /// <summary>
        /// The type of the module
        /// </summary>
        /// <value>The type.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Type
        {
            get; set;
        }

        /// <summary>
        /// The default culture of this tenant
        /// </summary>
        /// <value>The culture default.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string CultureDefault
        {
            get; set;
        }

        /// <summary>
        /// Each module can have an open-ended set of components within that provide the functionality
        /// </summary>
        /// <value>The components.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IList<Component<TIdType, TEntityIdType>> Components { get; set; }
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }

        #region "Constructors"
        /// <summary>
        /// Initializes a new instance of the <see cref="Module{TIdType, TEntityIdType}"/> class.
        /// </summary>
        public Module() : base()
        {
            Type = string.Empty;
            Components = new List<Component<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Module{TIdType, TEntityIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public Module(int? tenantId) : base()
        {
            Type = string.Empty;
            TenantId = tenantId;
            Components = new List<Component<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Module{TIdType, TEntityIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        public Module(int? tenantId, TIdType id, string cultureName) : base(id, cultureName)
        {
            Type = string.Empty;
            TenantId = tenantId;
            CultureDefault = cultureName;
            Components = new List<Component<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Module{TIdType, TEntityIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="cultureDefault">The culture default.</param>
        public Module(int? tenantId, TIdType id, string cultureName, String cultureDefault) : base(id, cultureName)
        {
            Type = string.Empty;
            CultureDefault = cultureDefault;
            Components = new List<Component<TIdType, TEntityIdType>>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected Module(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Type = info.GetString("Type");
            CultureDefault = info.GetString("CultureDefault");
            Components = (List<Component<TIdType, TEntityIdType>>)info.GetValue("Components", typeof(List<Component<TIdType, TEntityIdType>>));
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
            info.AddValue("Type", Type);
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
            sb.AppendFormat(" Type={0};", Type);
            sb.AppendFormat(" CultureDefault={0};", CultureDefault);
            sb.AppendFormat(" Components={0};", Components);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
