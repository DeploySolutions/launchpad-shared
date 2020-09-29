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
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public abstract partial class FileBase<TIdType> : DomainEntityBase<TIdType>, IFile<TIdType>
    {
        /// <summary>
        /// The FileKey that uniquely identifies this entity
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual FileKey Key { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public long Size { get; set; }
        
        [DataObjectField(false)]
        [XmlAttribute]
        public string Path { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Extension { get; }

        [DataObjectField(false)]
        [XmlAttribute]
        public byte[] Data { get; set; }

        protected FileBase()
        {
            Key = new FileKey();
        }

        protected FileBase(TIdType id) : base(id)
        {
            Id = id;
            Key = new FileKey();
        }
        protected FileBase(string fileName) : base()
        {
            Name = fileName;
            Key = new FileKey(fileName);
        }

        protected FileBase(TIdType id, string fileName) : base()
        {
            Id = id;
            Name = fileName;
            Key = new FileKey(fileName);
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileBase(SerializationInfo info, StreamingContext context) :base(info,context)
        {
            Key = (FileKey)info.GetValue("Key", typeof(FileKey));
            Data = (byte[])info.GetValue("Data", typeof(byte[]));
            Name = info.GetString("Name");
            Size = info.GetInt64("FileSize");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Key", Key);
            info.AddValue("FileSize", Size);
            info.AddValue("Name", Name);
            info.AddValue("Data", Data);
        }
    }
}
