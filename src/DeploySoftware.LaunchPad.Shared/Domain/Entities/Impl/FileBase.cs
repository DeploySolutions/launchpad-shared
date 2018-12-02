//LaunchPad Shared
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    public abstract partial class FileBase<TPrimaryKey> : Entity<TPrimaryKey>, IFile<TPrimaryKey>
    {
        /// <summary>
        /// The FileKey that uniquely identifies this entity
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual FileKey GlobalKey { get; set; }

        /// <summary>
        /// Each entity can have an open-ended set of metadata applied to it, that helps to describe it.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual MetadataInformation Metadata { get; set; }

        /// <summary>
        /// Each entity can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IEnumerable<MetadataTag<TPrimaryKey>> Tags { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public long FileSize { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string FileName { get; set; }
        
        [DataObjectField(false)]
        [XmlAttribute]
        public string FilePath { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string FileExtension { get; }

        [DataObjectField(false)]
        [XmlAttribute]
        public byte[] Data { get; set; }

        protected FileBase()
        {
            GlobalKey = new FileKey();
            Metadata = new MetadataInformation();
            Tags = new List<MetadataTag<TPrimaryKey>>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileBase(SerializationInfo info, StreamingContext context)
        {
            GlobalKey = (FileKey)info.GetValue("FileKey", typeof(FileKey));
            Metadata = (MetadataInformation)info.GetValue("Metadata", typeof(MetadataInformation));
            Tags = (IEnumerable<MetadataTag<TPrimaryKey>>)info.GetValue("Metadata", typeof(IEnumerable<MetadataTag<TPrimaryKey>>));
            Data = (byte[])info.GetValue("Data", typeof(byte[]));
            FileName = info.GetString("FileName");
            FileSize = info.GetInt64("FileSize");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GlobalKey", GlobalKey);
            info.AddValue("FileSize", FileSize);
            info.AddValue("FileName", FileName);
            info.AddValue("Data", Data);
            info.AddValue("Metadata", Metadata);
            info.AddValue("Tags", Tags);
        }
    }
}
