﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ReleaseCandidateEventBase.cs" company="Deploy Software Solutions, inc.">
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

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Deploy.LaunchPad.Core.Abp.Domain.Model;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Represents an event related to a release (set of code, data, and resources).
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract partial class ReleaseCandidateEventBase<TIdType> : TenantSpecificDomainEntityBase<TIdType>, IReleaseCandidateEvent<TIdType>
    {
        /// <summary>
        /// The id of the release candidate this deployment is for
        /// </summary>
        /// <value>The release candidate identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        [ForeignKey(nameof(ReleaseCandidateId))]
        public virtual TIdType ReleaseCandidateId { get; set; }

        /// <summary>
        /// The category of this release candidate event
        /// </summary>
        /// <value>The event category.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String EventCategory { get; set; }

        /// <summary>
        /// The event start date and time
        /// </summary>
        /// <value>The started.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? Started { get; set; }

        /// <summary>
        /// The event end date and time. May be null if the event is ongoing
        /// </summary>
        /// <value>The ended.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? Ended { get; set; }

        /// <summary>
        /// The URI where the release candidate event log is located
        /// </summary>
        /// <value>The log URI.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri LogUri { get; set; }



        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseCandidateEventBase{TIdType}"/> class.
        /// </summary>
        public ReleaseCandidateEventBase() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseCandidateEventBase{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The id of the tenant to which this entity belongs</param>
        public ReleaseCandidateEventBase(int tenantId) : base()
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseCandidateEventBase{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="text">The text.</param>
        public ReleaseCandidateEventBase(int tenantId, TIdType id, string cultureName, String text) : base(tenantId, id, cultureName)
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ReleaseCandidateEventBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ReleaseCandidateId = (TIdType)info.GetValue("ReleaseCandidateId", typeof(TIdType));
            LogUri = (Uri)info.GetValue("LogUri", typeof(Uri));
            EventCategory = info.GetString("EventCategory");
            Started = info.GetDateTime("Started");
            Ended = info.GetDateTime("Ended");
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
            info.AddValue("ReleaseCandidateId", ReleaseCandidateId);
            info.AddValue("EventCategory", EventCategory);
            info.AddValue("LogUri", LogUri);
            info.AddValue("Started", Started);
            info.AddValue("Ended", Ended);
        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ReleaseCandidateEventBase : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" ReleaseCandidateId={0};", ReleaseCandidateId);
            sb.AppendFormat(" EventCategory={0};", EventCategory);
            sb.AppendFormat(" LogUri={0};", LogUri);
            sb.AppendFormat(" Started={0};", Started);
            sb.AppendFormat(" Ended={0};", Ended);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
