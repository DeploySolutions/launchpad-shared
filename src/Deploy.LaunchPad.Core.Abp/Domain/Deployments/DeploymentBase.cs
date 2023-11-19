// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="DeploymentBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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
    /// Represents a deployment that will take a release candidate (set of code, data, and resources) and place it in a destination environment.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract partial class DeploymentBase<TIdType> : TenantSpecificDomainEntityBase<TIdType>, IDeployment<TIdType>
    {

        /// <summary>
        /// The release candidate this deployment is for
        /// </summary>
        /// <value>The release candidate identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        [ForeignKey(nameof(ReleaseCandidateId))]
        public virtual TIdType ReleaseCandidateId { get; set; }

        /// <summary>
        /// The id of the process that will be followed during the deployment (if known)
        /// </summary>
        /// <value>The deployment process identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(DeploymentProcessId))]
        public virtual TIdType DeploymentProcessId { get; set; }

        /// <summary>
        /// The intended deployment date and time
        /// </summary>
        /// <value>The date scheduled.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? DateScheduled { get; set; }

        /// <summary>
        /// The actual deployment date and time
        /// </summary>
        /// <value>The date deployed.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? DateDeployed { get; set; }


        /// <summary>
        /// The current state of the deployment
        /// </summary>
        /// <value>The state of the deployment.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DeploymentStates DeploymentState { get; set; }

        /// <summary>
        /// The person primarily responsible for doing the deployment (if known)
        /// </summary>
        /// <value>The primary deployer user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(PrimaryDeployerUserId))]
        public virtual long? PrimaryDeployerUserId { get; set; }

        /// <summary>
        /// The possible states in which this deployment can be
        /// </summary>
        public enum DeploymentStates
        {
            /// <summary>
            /// The not started
            /// </summary>
            Not_Started = 0,
            /// <summary>
            /// The in progress
            /// </summary>
            In_Progress = 1,
            /// <summary>
            /// The succeeded
            /// </summary>
            Succeeded = 2,
            /// <summary>
            /// The failed
            /// </summary>
            Failed = 3
        }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public DeploymentBase() : base()
        {
            DeploymentState = DeploymentStates.Not_Started;
        }


        /// <summary>
        /// Default constructor where the tenant id is known
        /// </summary>
        /// <param name="tenantId">The id of the tenant to which this entity belongs</param>
        public DeploymentBase(int tenantId) : base(tenantId)
        {
            DeploymentState = DeploymentStates.Not_Started;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentBase{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="text">The text.</param>
        public DeploymentBase(int tenantId, TIdType id, string cultureName, String text) : base(tenantId, id, cultureName)
        {
            DeploymentState = DeploymentStates.Not_Started;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DeploymentBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            PrimaryDeployerUserId = info.GetInt64("PrimaryDeployerUserId");
            ReleaseCandidateId = (TIdType)info.GetValue("ReleaseCandidateId", typeof(TIdType));
            DeploymentProcessId = (TIdType)info.GetValue("DeploymentProcessId", typeof(TIdType));
            DeploymentState = (DeploymentStates)info.GetValue("DeploymentState", typeof(DeploymentStates));
            DateDeployed = info.GetDateTime("DateDeployed");
            DateScheduled = info.GetDateTime("DateScheduled");
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
            info.AddValue("PrimaryDeployerUserId", PrimaryDeployerUserId);
            info.AddValue("ReleaseCandidateId", ReleaseCandidateId);
            info.AddValue("DeploymentProcessId", DeploymentProcessId);
            info.AddValue("DeploymentState", DeploymentState);
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
            sb.Append("[DeploymentBase : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" PrimaryDeployerUserId={0};", PrimaryDeployerUserId);
            sb.AppendFormat(" ReleaseCandidateId={0};", ReleaseCandidateId);
            sb.AppendFormat(" DeploymentProcessId={0};", DeploymentProcessId);
            sb.AppendFormat(" DeploymentState={0};", DeploymentState);
            sb.AppendFormat(" DateScheduled={0};", DateScheduled);
            sb.AppendFormat(" DateDeployed={0};", DateDeployed);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
