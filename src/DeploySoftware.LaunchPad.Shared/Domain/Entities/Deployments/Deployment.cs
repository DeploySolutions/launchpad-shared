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
        /// The intended deployment date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? ScheduledDeploymentDate { get; set; }

        /// <summary>
        /// The actual deployment date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? ActualDeploymentDate { get; set; }


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
        protected Deployment(SerializationInfo info, StreamingContext context)
        {
    
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
            info.AddValue("ScheduledDeploymentDate", ScheduledDeploymentDate); 
            info.AddValue("ActualDeploymentDate", ActualDeploymentDate);            
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
            sb.AppendFormat(" ScheduledDeploymentDate={0};", ScheduledDeploymentDate); 
            sb.AppendFormat(" ActualDeploymentDate={0};", ActualDeploymentDate);            
            sb.Append("]");
            return sb.ToString();
        }
    }
}
