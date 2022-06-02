//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public abstract partial class FileStorageLocationBase : IFileStorageLocation
    {
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Id { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; set; }

        /// <summary>
        /// A short description for this storage location
        /// </summary>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this storage location
        /// </summary>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public virtual string DescriptionFull { get; set; }

        [DataObjectField(true)]
        [XmlAttribute]
        public virtual bool IsReadOnly { get; set; } = false;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri RootUri { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DefaultPrefix { get; set; } = string.Empty;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual FileStorageProviderTypeEnum Provider { get; set; }

        /// <summary>
        /// The location have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IEnumerable<MetadataTag> Tags { get; set; }

        protected FileStorageLocationBase()
        {
            Tags = new List<MetadataTag>();
        }

        public FileStorageLocationBase(string id, Uri rootUri)
        {
            Id = id;
            Name = id;
            RootUri = rootUri;
            Tags = new List<MetadataTag>();
        }

        public FileStorageLocationBase(string id, Uri rootUri, FileStorageProviderTypeEnum provider)
        {
            Id = id;
            Name = id;
            RootUri = rootUri;
            Provider = provider; 
            Tags = new List<MetadataTag>();
        }

        public FileStorageLocationBase(string id, string name, Uri rootUri, FileStorageProviderTypeEnum provider)
        {
            Id = id;
            Name = name;
            RootUri = rootUri;
            Provider = provider;
            Tags = new List<MetadataTag>();
        }

        public FileStorageLocationBase(string id, string name, Uri rootUri, FileStorageProviderTypeEnum provider, IEnumerable<MetadataTag> tags)
        {
            Id = id;
            Name = name;
            RootUri = rootUri;
            Provider = provider;
            Tags = tags;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileStorageLocationBase(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetString(Id); 
            Name = info.GetString(Name); 
            DescriptionShort = info.GetString("DescriptionShort");
            DescriptionFull = info.GetString("DescriptionFull");
            IsReadOnly = info.GetBoolean("IsReadOnly");
            RootUri = (Uri)info.GetValue("RootUri", typeof(Uri));
            Provider = (FileStorageProviderTypeEnum)info.GetValue("Provider", typeof(FileStorageProviderTypeEnum));
            DefaultPrefix = info.GetString("DefaultPrefix"); 
            Tags = (IEnumerable<MetadataTag>)info.GetValue("Metadata", typeof(IEnumerable<MetadataTag>));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Name", Name); 
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("DescriptionFull", DescriptionFull);
            info.AddValue("IsReadOnly", IsReadOnly);
            info.AddValue("RootUri", RootUri);
            info.AddValue("Provider", Provider);
            info.AddValue("DefaultPrefix", DefaultPrefix); 
            info.AddValue("Tags", Tags);
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(FileStorageLocationBase other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return Name.CompareTo(other.Name);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DomainEntityBase: ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected virtual String ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            // LaunchPAD RAD properties
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("Provider={0};", Provider);
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat(" Tags={0};", Tags.ToString());
            sb.AppendFormat("RootUri={0};", RootUri);
            sb.AppendFormat("IsReadOnly={0};", IsReadOnly);
            sb.AppendFormat("DefaultPrefix={0};", DefaultPrefix);
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is FileStorageLocationBase)
            {
                return Equals(obj as FileStorageLocationBase);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.        
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns></returns>
        public virtual bool Equals(FileStorageLocationBase obj)
        {
            if (obj != null)
            {

                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return Id.Equals(obj.Id) && Name.Equals(obj.Name) && Provider.Equals(obj.Provider)
                    && DefaultPrefix.Equals(obj.DefaultPrefix) && RootUri.Equals(obj.RootUri);
     
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(FileStorageLocationBase x, FileStorageLocationBase y)
        {
            if (x is null)
            {
                if (y is null)
                {
                    return true;
                }
                return false;
            }
            return x.Equals(y);
        }

        /// <summary>
        /// Override the != operator to test for inequality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are not equal based on the Equals logic</returns>
        public static bool operator !=(FileStorageLocationBase x, FileStorageLocationBase y)
        {
            return !(x == y);
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
            return Id.GetHashCode()
                + Name.GetHashCode()
                + Provider.GetHashCode()
                + RootUri.GetHashCode();
            ;
        }

    }
}
