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

using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public abstract partial class FileBase<TIdType, TFileStorageLocationType> : DomainEntityBase<TIdType>, IFile<TIdType, TFileStorageLocationType>
        where TFileStorageLocationType: IFileStorageLocation, new()
    {

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual long Size { get; set; }        

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Extension { get; set;  }

        /// <summary>
        /// The content / mime type of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String MimeType { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual TFileStorageLocationType Location { get; set; }

        protected FileBase()
        {
            Location = new TFileStorageLocationType();
        }

        protected FileBase(TIdType id) : base(id)
        {
            Id = id;
            Location = new TFileStorageLocationType();
        }
        protected FileBase(string fileName) : base()
        {
            Name = fileName;
            Location = new TFileStorageLocationType();
        }

        protected FileBase(TIdType id, string fileName) : base()
        {
            Id = id;
            Name = fileName;
            Location = new TFileStorageLocationType();
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileBase(SerializationInfo info, StreamingContext context) :base(info,context)
        {
            Location = (TFileStorageLocationType)info.GetValue("Data", typeof(TFileStorageLocationType));
            Name = info.GetString("Name");
            Size = info.GetInt64("Size");
            MimeType = info.GetString("MimeType");
            Extension = info.GetString("Extension");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Size", Size);
            info.AddValue("Name", Name);
            info.AddValue("MimeType", MimeType);
            info.AddValue("Extension", Extension);
            info.AddValue("Location", Location);
        }


        /// <summary>
        /// The full path of the file
        /// </summary>
        public Uri GetFullPathUri()
        {
            return Location.GetFullPathUri(Name);
        }

    }
}
