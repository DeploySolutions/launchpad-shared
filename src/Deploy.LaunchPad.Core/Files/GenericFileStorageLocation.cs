//LaunchPad Shared
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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

using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain
{
    [DebuggerDisplay("{_debugDisplay}")]
    [Serializable]
    public partial class GenericFileStorageLocation : IFileStorageLocation
    {
        /// <summary>
        /// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        /// </summary>
        protected virtual string _debugDisplay => $"{Id}. Name {Name}.";

        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Id { get; set; } = string.Empty;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// A short description for this storage location
        /// </summary>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionShort { get; set; } = string.Empty;

        /// <summary>
        /// The full description for this storage location
        /// </summary>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public virtual string DescriptionFull { get; set; } = string.Empty;

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
        public virtual FileStorageLocationTypeEnum Provider { get; set; }

        /// <summary>
        /// Reference to the logger to write logs.
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// The location have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IEnumerable<MetadataTag> Tags { get; set; }

        public GenericFileStorageLocation()
        {
            Name = "Generic File Storage Location";
            Id = "GenericFile1";
            Provider = FileStorageLocationTypeEnum.Unknown;
            Tags = new List<MetadataTag>();
            Logger = NullLogger.Instance;
        }

        public GenericFileStorageLocation(ILogger logger, string id, Uri rootUri)
        {
            Id = id;
            Name = id;
            RootUri = rootUri;
            Provider = FileStorageLocationTypeEnum.Unknown;
            Tags = new List<MetadataTag>();
            Logger = logger;
        }
        public GenericFileStorageLocation(ILogger logger)
        {
            Name = "Generic File Storage Location";
            Id = "GenericFile1";
            Provider = FileStorageLocationTypeEnum.Unknown;
            Tags = new List<MetadataTag>();
            Logger = logger;
        }

        public GenericFileStorageLocation(ILogger logger, string id, Uri rootUri, FileStorageLocationTypeEnum provider)
        {
            Id = id;
            Name = id;
            RootUri = rootUri;
            Provider = provider;
            Tags = new List<MetadataTag>();
            Logger = logger;
        }

        public GenericFileStorageLocation(ILogger logger, string id, string name, Uri rootUri, FileStorageLocationTypeEnum provider)
        {
            Id = id;
            Name = name;
            RootUri = rootUri;
            Provider = provider;
            Tags = new List<MetadataTag>();
            Logger = logger;
        }

        public GenericFileStorageLocation(ILogger logger, string id, string name, Uri rootUri, FileStorageLocationTypeEnum provider, IEnumerable<MetadataTag> tags)
        {
            Id = id;
            Name = name;
            RootUri = rootUri;
            Provider = provider;
            Tags = tags;
            Logger = logger;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GenericFileStorageLocation(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetString(Id);
            Name = info.GetString(Name);
            DescriptionShort = info.GetString("DescriptionShort");
            DescriptionFull = info.GetString("DescriptionFull");
            IsReadOnly = info.GetBoolean("IsReadOnly");
            RootUri = (Uri)info.GetValue("RootUri", typeof(Uri));
            Provider = (FileStorageLocationTypeEnum)info.GetValue("Provider", typeof(FileStorageLocationTypeEnum));
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
        public virtual int CompareTo(GenericFileStorageLocation other)
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
            sb.Append("[GenericFileStorageLocation: ");
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
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("Provider={0};", Provider);
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("RootUri={0};", RootUri);
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
            if (obj != null && obj is GenericFileStorageLocation)
            {
                return Equals(obj as GenericFileStorageLocation);
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
        public virtual bool Equals(GenericFileStorageLocation obj)
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
        public static bool operator ==(GenericFileStorageLocation x, GenericFileStorageLocation y)
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
        public static bool operator !=(GenericFileStorageLocation x, GenericFileStorageLocation y)
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
        
        
        
        public virtual bool CreateFile<TFile, TFileId, TFileContentType>(TFile sourceFile, IDictionary<string, string> fileTags, string contentType, IDictionary<string, string> writeTags, string filePrefix, string fileSuffix)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            return CreateFileAsync<TFile, TFileId, TFileContentType>(sourceFile, fileTags, contentType, writeTags, filePrefix, fileSuffix).Result;
        }

        public virtual async Task<bool> CreateFileAsync<TFile, TFileId, TFileContentType>(TFile sourceFile, IDictionary<string, string> fileTags, string contentType, IDictionary<string, string> writeTags, string filePrefix, string fileSuffix)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            throw new NotImplementedException();
        }

        public virtual TFile ReadFile<TFile, TFileId, TFileContentType>(string fileId, Uri tempLocation = null)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            return ReadFileAsync<TFile, TFileId, TFileContentType>(fileId, tempLocation).Result;
        }

        public virtual async Task<TFile> ReadFileAsync<TFile, TFileId, TFileContentType>(string fileId, Uri tempLocation = null)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            throw new NotImplementedException();
        }

        public virtual IDictionary<string, string> ReadFileMetadata<TFile, TFileId, TFileContentType>(TFile sourceFile)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            return ReadFileMetadataAsync<TFile, TFileId, TFileContentType>(sourceFile).Result;
        }

        public virtual async Task<IDictionary<string, string>> ReadFileMetadataAsync<TFile, TFileId, TFileContentType>(TFile sourceFile)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            throw new NotImplementedException();
        }


        public virtual TFile UpdateFile<TFile, TFileId, TFileContentType>(TFile fileToUpdate)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            return UpdateFileAsync<TFile, TFileId, TFileContentType>(fileToUpdate).Result;
        }
        public virtual async Task<TFile> UpdateFileAsync<TFile, TFileId, TFileContentType>(TFile fileToUpdate)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            throw new NotImplementedException();
        }



        public virtual bool DeleteFile<TFile, TFileId, TFileContentType>(TFile fileToDelete)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            return DeleteFileAsync<TFile, TFileId, TFileContentType>(fileToDelete).Result;
        }

        public virtual async Task<bool> DeleteFileAsync<TFile, TFileId, TFileContentType>(TFile fileToDelete)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {

            throw new NotImplementedException();
        }


        public virtual Uri GetRelativePathForFile<TFile, TFileId, TFileContentType>(TFile file)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            throw new NotImplementedException();
        }

        public virtual Uri GetFullPathForFile<TFile, TFileId, TFileContentType>(TFile file)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns available storage space for this location, in bytes, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns></returns>
        public virtual long GetAvailableStorageSpaceInBytes()
        {
            return -1;
        }


        /// <summary>
        /// Returns available storage space for this location, in GB, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns></returns>
        public virtual double GetAvailableStorageSpaceInGigabytes()
        {
            return -1;
        }

        public virtual bool FileExists<TFile, TFileId, TFileContentType>(TFile fileToCheck, bool shouldRecurseSubdirectories = false)
            where TFile : IFile<TFileId, TFileContentType>, new()
        {
            throw new NotImplementedException();
        }
    }
}
