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
    public class Deployment<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IDeployment<TPrimaryKey>
    {

        /// <summary>
        /// The release candidate this deployment is for
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public TPrimaryKey ReleaseCandidateId { get; set; }

        /// <summary>
        /// The id of the process that will be followed during the deployment (if known)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public TPrimaryKey DeploymentProcessId { get; set; }

        /// <summary>
        /// The current state of the deployment
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String State { get; set; }

        /// <summary>
        /// The intended deployment date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? DateScheduled { get; set; }

        /// <summary>
        /// The actual deployment date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? DateDeployed { get; set; }

        /// <summary>
        /// The person primarily responsible for doing the deployment (if known)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public long? PrimaryDeployerUserId { get; set; }

        #region "Constructors"

        public Deployment() : base()
        {
         
        }

        public Deployment(string cultureName, String text) : base(cultureName)
        {

        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected Deployment(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            State = info.GetString("State");
            PrimaryDeployerUserId = info.GetInt64("PrimaryDeployerUserId");
            ReleaseCandidateId = (TPrimaryKey)info.GetValue("ReleaseCandidateId", typeof(TPrimaryKey));
            DeploymentProcessId = (TPrimaryKey)info.GetValue("DeploymentProcessId", typeof(TPrimaryKey));
            DateDeployed = info.GetDateTime("DateDeployed");
            DateScheduled = info.GetDateTime("DateScheduled");
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
            info.AddValue("State", State); 
            info.AddValue("PrimaryDeployerUserId", PrimaryDeployerUserId);
            info.AddValue("ReleaseCandidateId", ReleaseCandidateId);
            info.AddValue("DeploymentProcessId", DeploymentProcessId);
            info.AddValue("DateDeployed", DateDeployed);
            info.AddValue("DateScheduled", DateScheduled);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Deployment : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" State={0};", State);
            sb.AppendFormat(" PrimaryDeployerUserId={0};", PrimaryDeployerUserId);
            sb.AppendFormat(" ReleaseCandidateId={0};", ReleaseCandidateId);
            sb.AppendFormat(" DeploymentProcessId={0};", DeploymentProcessId);
            sb.AppendFormat(" DateScheduled={0};", DateScheduled); 
            sb.AppendFormat(" DateDeployed={0};", DateDeployed);            
            sb.Append("]");
            return sb.ToString();
        }
    }
}
