// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Component.cs" company="Deploy Software Solutions, inc.">
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
using Deploy.LaunchPad.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using IMayHaveTenant = Deploy.LaunchPad.Core.Entities.IMayHaveTenant;


namespace Deploy.LaunchPad.Core.Abp.SoftwareApplications
{
    /// <summary>
    /// Base class for components
    /// </summary>
    /// <typeparam name="TIdType">The type of the key id field</typeparam>
    /// <typeparam name="TEntityIdType">The type of the t entity identifier type.</typeparam>
    [Serializable()]
    public partial class Component<TIdType, TEntityIdType> : LaunchPadDomainEntityBase<TIdType>, IComponent<TIdType, TEntityIdType>, IMayHaveTenant
    {

        /// <summary>
        /// Each component can have 0 to many domain entities
        /// </summary>
        /// <value>The domain entities.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IList<LaunchPadDomainEntityBase<TEntityIdType>> DomainEntities { get; set; }
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }

        #region "Constructors"
        /// <summary>
        /// Initializes a new instance of the <see cref="Component{TIdType, TEntityIdType}"/> class.
        /// </summary>
        public Component() : base()
        {
            DomainEntities = new List<LaunchPadDomainEntityBase<TEntityIdType>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Component{TIdType, TEntityIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public Component(int? tenantId) : base()
        {
            TenantId = tenantId;
            DomainEntities = new List<LaunchPadDomainEntityBase<TEntityIdType>>();
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected Component(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DomainEntities = (IList<LaunchPadDomainEntityBase<TEntityIdType>>)info.GetValue("DomainEntities", typeof(List<LaunchPadDomainEntityBase<TEntityIdType>>));
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
            info.AddValue("DomainEntities", DomainEntities);
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
            sb.AppendFormat(" DomainEntities={0};", DomainEntities);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
