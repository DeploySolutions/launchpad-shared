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
    public class DeploymentProcess<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IDeploymentProcess<TPrimaryKey>
    {
        /// <summary>
        /// The URI to the deployment documentation
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public Uri DocumentationUrl { get; set; }

        /// <summary>
        /// The URI to the diagram
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public Uri DiagramUrl { get; set; }


        #region "Constructors"

        public DeploymentProcess(int? tenantId) : base(tenantId)
        {
         
        }

        public DeploymentProcess(int? tenantId, string cultureName, String text) : base(tenantId, cultureName)
        {

        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DeploymentProcess(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DocumentationUrl = (Uri)info.GetValue("DocumentationUrl", typeof(Uri));
            DiagramUrl = (Uri)info.GetValue("DiagramUrl", typeof(Uri));
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
            info.AddValue("DiagramUrl", DiagramUrl);
            info.AddValue("DocumentationUrl", DocumentationUrl);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DeploymentProcess : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" DocumentationUrl={0};", DocumentationUrl);
            sb.AppendFormat(" DiagramUrl={0};", DiagramUrl);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
