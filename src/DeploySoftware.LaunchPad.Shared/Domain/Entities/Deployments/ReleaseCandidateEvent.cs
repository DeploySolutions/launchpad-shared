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
    
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Shared.Domain
{
    /// <summary>
    /// Represents an event related to a release (set of code, data, and resources).
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public class ReleaseCandidateEvent<TIdType> : TenantSpecificDomainEntityBase<TIdType>, IReleaseCandidateEvent<TIdType>
    {
        /// <summary>
        /// The id of the release candidate this deployment is for
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        [ForeignKey(nameof(ReleaseCandidateId))]
        public TIdType ReleaseCandidateId { get; set; }

        /// <summary>
        /// The category of this release candidate event
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String EventCategory { get; set; }

        /// <summary>
        /// The event start date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? Started { get; set; }

        /// <summary>
        /// The event end date and time. May be null if the event is ongoing
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? Ended { get; set; }

        /// <summary>
        /// The URI where the release candidate event log is located
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public Uri LogUri { get; set; }



        #region "Constructors"

        public ReleaseCandidateEvent(int tenantId) : base(tenantId)
        {
         
        }

        public ReleaseCandidateEvent(int tenantId, TIdType id, string cultureName, String text) : base(tenantId, id, cultureName)
        {

        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ReleaseCandidateEvent(SerializationInfo info, StreamingContext context) : base(info, context)
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
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
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
            sb.Append("[ReleaseCandidateEvent : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" ReleaseCandidateId={0};", ReleaseCandidateId);
            sb.AppendFormat(" EventCategory={0};", EventCategory);
            sb.AppendFormat(" LogUri={0};", LogUri);
            sb.AppendFormat(" Started={0};", Started);
            sb.AppendFormat(" Ended={0};", Ended);
            sb.Append("]");
            return sb.ToString();
        }
    }
}