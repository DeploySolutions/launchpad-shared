// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ContentItem.cs" company="Deploy Software Solutions, inc.">
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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Domain;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;


namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Represents some text
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    [Serializable()]
    public partial class ContentItem<TIdType> : LaunchPadDomainEntityBase<TIdType>, IContentItem<TIdType>, IMayHaveTenant
    {
        /// <summary>
        /// The textual content
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Content
        {
            get; set;
        }
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public int? TenantId { get; set; }


        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentItem{TIdType}"/> class.
        /// </summary>
        public ContentItem() : base()
        {
            Content = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentItem{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public ContentItem(int tenantId) : base()
        {
            Content = String.Empty;
            TenantId = tenantId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentItem{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="text">The text.</param>
        public ContentItem(int tenantId, TIdType id, string cultureName, String text) : base(id, cultureName)
        {
            Content = text;
            TenantId = tenantId;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ContentItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Content = info.GetString("Text");
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
            info.AddValue("Text", Content);
        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ContentItem : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" Text={0};", Content);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
