// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ReleaseCandidateBase.cs" company="Deploy Software Solutions, inc.">
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

using Deploy.LaunchPad.Code.Deployments;
using Deploy.LaunchPad.Core.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace Deploy.LaunchPad.Core.Abp.Deployments
{
    /// <summary>
    /// Represents a release (set of code, data, and resources) that is a candidate to be deployed to a destination environment.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the Id</typeparam>
    public abstract partial class ReleaseCandidateBase<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IReleaseCandidate<TPrimaryKey>
    {

        /// <summary>
        /// The version of this release
        /// </summary>
        /// <value>The version.</value>
        public virtual string Version { get; set; }
        /// <summary>
        /// The current state of the release candidate
        /// </summary>
        /// <value>The state of the release.</value>
        public virtual ReleaseState ReleaseState { get; set; }

        /// <summary>
        /// The date and time this release occurred
        /// </summary>
        /// <value>The release date.</value>
        public virtual DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// The URI to the release package
        /// </summary>
        /// <value>The package URI.</value>
        public virtual Uri PackageUri { get; set; }

        

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseCandidateBase{TPrimaryKey}"/> class.
        /// </summary>
        public ReleaseCandidateBase() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseCandidateBase{TPrimaryKey}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="text">The text.</param>
        [SetsRequiredMembers]
        public ReleaseCandidateBase(TPrimaryKey id, string cultureName, String text) : base(id, cultureName)
        {
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
            ReleaseState = (ReleaseState)info.GetValue("ReleaseState", typeof(ReleaseState));
            PackageUri = (Uri)info.GetValue("PackageUri", typeof(Uri));
            ReleaseDate = info.GetDateTime("ReleaseDate");
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
