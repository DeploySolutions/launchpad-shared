namespace DeploySoftware.LaunchPad.Shared.Domain
{

    //LaunchPad Shared
    // Copyright (c) 2016 Deploy Software Solutions, inc. 

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


    [Serializable()]
    public class MetadataTag<TPrimaryKey> : TagBase<TPrimaryKey>, ILaunchPadMetadataTag<TPrimaryKey>
    {
        
#region "Constructors"

        public MetadataTag()
        {
            GlobalKey = new DomainEntityKey();
            Metadata = new MetadataInformation();
            Name = String.Empty;
            Value = null;
            Scheme = String.Empty;
        }

        public MetadataTag(String name, String value)
        {
            GlobalKey = new DomainEntityKey();
            Metadata = new MetadataInformation();
            Name = name;
            Value = value;
            Scheme = String.Empty;
        }
        public MetadataTag(String name, String value, String scheme)
        {
            GlobalKey = new DomainEntityKey();
            Metadata = new MetadataInformation();
            Name = name;
            Value = value;
            Scheme = scheme;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected MetadataTag(SerializationInfo info, StreamingContext context)
        {
            GlobalKey = (DomainEntityKey)info.GetValue("GlobalKey", typeof(DomainEntityKey));
            Metadata = (MetadataInformation)info.GetValue("Metadata", typeof(MetadataInformation));
            Name = info.GetString("Name");
            Value = info.GetString("Value");
            Scheme = info.GetString("Scheme");
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
            info.AddValue("Name", Name);
            info.AddValue("Scheme", Scheme);
            info.AddValue("Value", Value);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Tag : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.Append("]");
            return sb.ToString();
        }
    }
}
