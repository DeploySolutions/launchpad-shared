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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public abstract partial class FileStorageLocationBase : IFileStorageLocation
    {
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri RootPath { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public virtual byte[] Data { get; set; }

        protected FileStorageLocationBase()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileStorageLocationBase(SerializationInfo info, StreamingContext context)
        {
            RootPath = (Uri)info.GetValue("RootPath", typeof(Uri)); 
            Data = (byte[])info.GetValue("Data", typeof(byte[]));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RootPath", RootPath); 
            info.AddValue("Data", Data);
        }


    }
}
