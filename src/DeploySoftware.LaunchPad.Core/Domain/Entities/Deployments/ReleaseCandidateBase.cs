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
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Domain
{
    /// <summary>
    /// Represents a release (set of code, data, and resources) that is a candidate to be deployed to a destination environment.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract partial class ReleaseCandidateBase<TIdType> : TenantSpecificDomainEntityBase<TIdType>, IReleaseCandidate<TIdType>
    {

        /// <summary>
        /// The checksum (from git or elsewhere)
        /// </summary>
        public virtual string Checksum { get; set; }

        /// <summary>
        /// The version of this release
        /// </summary>
        public virtual string Version { get; set; }
        public virtual ReleaseStates ReleaseState { get; set; }

        /// <summary>
        /// The date and time this release occurred
        /// </summary>
        public virtual DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// The URI to the release package
        /// </summary>
        public virtual Uri PackageUri { get; set; }

        public enum ReleaseStates
        {
            Not_Started = 0,
            In_Progress = 1,
            Succeeded = 2,
            Failed = 3
        }


        #region "Constructors"

        public ReleaseCandidateBase() : base()
        {

        }


        public ReleaseCandidateBase(int tenantId) : base()
        {
            TenantId = tenantId;
        }

        public ReleaseCandidateBase(int tenantId, TIdType id, string cultureName, String text) : base(tenantId, id, cultureName)
        {
            TenantId = tenantId;
        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ReleaseCandidateBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Version = info.GetString("Version");
            Checksum = info.GetString("Checksum");
            ReleaseState = (ReleaseStates)info.GetValue("ReleaseState", typeof(ReleaseStates));
            PackageUri = (Uri)info.GetValue("PackageUri", typeof(Uri));
            ReleaseDate = info.GetDateTime("ReleaseDate");
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
            info.AddValue("Version", Version);
            info.AddValue("Checksum", Checksum);
            info.AddValue("ReleaseState", ReleaseState);
            info.AddValue("PackageUri", PackageUri);
            info.AddValue("ReleaseDate", ReleaseDate);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ReleaseCandidateBase : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" Version={0};", Version);
            sb.AppendFormat(" Checksum={0};", Checksum);
            sb.AppendFormat(" ReleaseState={0};", ReleaseState);
            sb.AppendFormat(" PackageUri={0};", PackageUri);
            sb.AppendFormat(" ReleaseDate={0};", ReleaseDate);       
            sb.Append(']');
            return sb.ToString();
        }
    }
}
