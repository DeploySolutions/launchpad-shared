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
using System.Text;
using System.Xml.Serialization;
using Deploy.LaunchPad.Core.Abp.Domain.Model;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Represents the process which a deployment will follow as it takes a release candidate (set of code, data, and resources) and places it in a destination environment.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract partial class DeploymentProcessBase<TIdType> : LaunchPadDomainEntityBase<TIdType>, IDeploymentProcess<TIdType>
    {
        /// <summary>
        /// The URI to the deployment documentation
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri DocumentationUri { get; set; }

        /// <summary>
        /// The URI to the diagram
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri DiagramUri { get; set; }


        #region "Constructors"

        protected DeploymentProcessBase() : base()
        {
        }


        protected DeploymentProcessBase(TIdType id, string cultureName, String text) : base(id, cultureName)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DeploymentProcessBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DocumentationUri = (Uri)info.GetValue("DocumentationUrl", typeof(Uri));
            DiagramUri = (Uri)info.GetValue("DiagramUrl", typeof(Uri));
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
            info.AddValue("DiagramUrl", DiagramUri);
            info.AddValue("DocumentationUrl", DocumentationUri);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DeploymentProcessBase : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" DocumentationUrl={0};", DocumentationUri);
            sb.AppendFormat(" DiagramUrl={0};", DiagramUri);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
