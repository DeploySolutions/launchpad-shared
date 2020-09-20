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
    
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Shared.Domain
{
    public class ReleaseCandidate<TIdType> : TenantSpecificDomainEntityBase<TIdType>, IReleaseCandidate<TIdType>
    {


        public string Checksum { get; set; }
        public string Version { get; set; }
        public string State { get; set; }
        public DateTime? DateReleased { get; set; }
        public Uri PackageUri { get; set; }


        #region "Constructors"

        public ReleaseCandidate(int tenantId) : base(tenantId)
        {
         
        }

        public ReleaseCandidate(int tenantId, TIdType id, string cultureName, String text) : base(tenantId, id, cultureName)
        {

        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ReleaseCandidate(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Version = info.GetString("Version");
            Checksum = info.GetString("Checksum");
            State = info.GetString("State");
            PackageUri = (Uri)info.GetValue("PackageUri", typeof(Uri));
            DateReleased = info.GetDateTime("DateReleased");
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
            info.AddValue("Version", Version);
            info.AddValue("Checksum", Checksum);
            info.AddValue("State", State);
            info.AddValue("PackageUri", PackageUri);
            info.AddValue("DateReleased", DateReleased);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ReleaseCandidate : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" Version={0};", Version);
            sb.AppendFormat(" Checksum={0};", Checksum);
            sb.AppendFormat(" State={0};", State);
            sb.AppendFormat(" PackageUri={0};", PackageUri);
            sb.AppendFormat(" DateReleased={0};", DateReleased);       
            sb.Append("]");
            return sb.ToString();
        }
    }
}
