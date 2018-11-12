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

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    using DeploySoftware.LaunchPad.Shared.Util;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// This class holds common metadata information for entities used by the framework,
    /// such as the author or the date last modified. It is a core component of any Entity class.
    /// </summary>
    [Serializable()]
    [ComplexType]
    public partial class MetadataInformation : IMetadataInformation
    {
        /// <summary>
        /// The unique id of this metadata tag
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Guid UniqueId { get; set; }

        /// <summary>
        /// The id of the User Agent which created this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Creator { get; set; }
           
        /// <summary>
        /// A description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Description { get; set; }

        /// <summary>
        /// The display name that can be displayed as a label externally to users when referring to this object
        /// (rather than using a GUID, which is unfriendly but unique)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DisplayName { get; set; }

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime DateLastModified { get; set; }

        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String LastModifiedBy { get; set; }

        /// <summary>
        /// Each entity in the framework can have a MIME type which is used to help display
        /// its information to Http-capable browsers. 
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String MimeType { get; set; }


        #region Constructors

        /// <summary>  
        /// Initializes a new instance of the <see cref="MetadataInformation">MetadataInformation</see> class.  
        /// </summary>
        public MetadataInformation()
        {
            //UniqueId = SequentialGuid.Generate(SequentialGuid.SequentialGuidType.SequentialAsString);
            UniqueId = Guid.NewGuid();
            Creator = String.Empty;
            Description = String.Empty;
            DisplayName = String.Empty;

            LastModifiedBy = String.Empty;
            DateCreated = DateTime.Now;
            DateLastModified = DateTime.Now;
            MimeType = String.Empty;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public MetadataInformation(SerializationInfo info, StreamingContext context)
        {
            UniqueId = new Guid(info.GetString("UniqueId"));
            MimeType = info.GetString("MimeType");
            DisplayName = info.GetString("DisplayName");
            Description = info.GetString("Description");
            Creator = info.GetString("Creator");            
            LastModifiedBy = info.GetString("LastModifiedBy");
            DateCreated = info.GetDateTime("DateCreated");
            DateLastModified = info.GetDateTime("DateLastModified");       
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UniqueId", UniqueId);
            info.AddValue("MimeType", MimeType);
            info.AddValue("DisplayName", DisplayName);
            info.AddValue("Description", Description);            
            info.AddValue("LastModifiedBy", LastModifiedBy);
            info.AddValue("Creator", Creator);
            info.AddValue("DateCreated", DateCreated);
            info.AddValue("DateLastModified", DateLastModified);
        }

        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other 
        /// <summary>finite resources that 
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        public virtual void OnDeserialization(object sender)
        {
            // reconnect connection strings and other resources that won't be serialized
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[MetadataInformation: ");
            sb.AppendFormat("UniqueId={0};", UniqueId);
            sb.AppendFormat("Creator={0};", Creator);
            sb.AppendFormat(" DisplayName={0};", DisplayName);
            if (!String.IsNullOrEmpty(Description))
            {
                sb.AppendFormat(" Description={0};", Description);
            }            
            sb.AppendFormat(" LastModifiedBy={0};", LastModifiedBy);
            if (!String.IsNullOrEmpty(MimeType))
            {
                sb.AppendFormat(" MimeType={0};", MimeType);
            }
            sb.AppendFormat(" DateCreated={0};", DateCreated);
            sb.AppendFormat(" DateLastModified={0};", DateLastModified); 
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast objectToSerialize in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) the right type</param>
        /// <returns>True if the objects are the same</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is MetadataInformation)
            {
                return Equals(obj as MetadataInformation);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="obj">The other object of this type we are testing equality with</param>
        /// <returns>True if the objects are equal in value</returns>
        public virtual bool Equals(MetadataInformation obj)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(obj, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
           return 
                (UniqueId.Equals(obj.UniqueId)
                    && Creator.Equals(obj.Creator)
                    && DisplayName.Equals(obj.DisplayName)
                    && Description.Equals(obj.Description)
                    && LastModifiedBy.Equals(obj.LastModifiedBy)
                    && MimeType.Equals(obj.MimeType) &&
                    DateCreated.Equals(obj.DateCreated) &&
                    DateLastModified.Equals(obj.DateLastModified)
                )
            ;
        }

        /// <summary>  
        /// Computes and retrieves a hash code for an object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return UniqueId.GetHashCode() 
                + Creator.GetHashCode() 
                + DisplayName.GetHashCode() 
                + Description.GetHashCode() 
                + LastModifiedBy.GetHashCode() 
                + MimeType.GetHashCode() 
                + DateCreated.GetHashCode() 
                + DateLastModified.GetHashCode();
        }
    }
}
