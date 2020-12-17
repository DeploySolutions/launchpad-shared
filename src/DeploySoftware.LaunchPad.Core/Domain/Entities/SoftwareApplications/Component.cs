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
    /// Base class for components
    /// </summary>
    /// <typeparam name="TIdType">The type of the key id field</typeparam>
    [Serializable()]
    public partial class Component<TIdType,TEntityIdType> : DomainEntityBase<TIdType>, IComponent<TIdType, TEntityIdType>, IMayHaveTenant
    {

        /// <summary>
        /// Each component can have 0 to many domain entities
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<DomainEntityBase<TEntityIdType>> DomainEntities { get; set; }
        public int? TenantId { get; set; }

        #region "Constructors"
        public Component() : base()
        {
            DomainEntities = new List<DomainEntityBase<TEntityIdType>>();
        }

        public Component(int? tenantId) : base()
        {
            TenantId = tenantId;
            DomainEntities = new List<DomainEntityBase<TEntityIdType>>();
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected Component(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            DomainEntities = (IEnumerable<DomainEntityBase<TEntityIdType>>)info.GetValue("DomainEntities", typeof(IEnumerable<DomainEntityBase<TEntityIdType>>));
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
            sb.Append("]");
            return sb.ToString();
        }
    }
}
