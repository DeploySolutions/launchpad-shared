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

using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Domain
{
    [Serializable()]
    public class ContentItem<TIdType> : DomainEntityBase<TIdType>, IContentItem<TIdType>
    {
        /// <summary>
        /// The name of this metadata tag
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Text
        {
            get;set;
        }
        public int TenantId { get; set; }


        #region "Constructors"

        public ContentItem() : base()
        {
            Text = String.Empty;
        }

        public ContentItem(int tenantId) : base()
        {
            Text = String.Empty;
            TenantId = tenantId;
        }

        public ContentItem(int tenantId, TIdType id, string cultureName, String text) : base(id, cultureName)
        {
            Text = text;
            TenantId = tenantId;
        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ContentItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Text = info.GetString("Text");
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
            info.AddValue("Text", Text);            
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
            sb.AppendFormat(" Text={0};", Text);            
            sb.Append(']');
            return sb.ToString();
        }
    }
}
