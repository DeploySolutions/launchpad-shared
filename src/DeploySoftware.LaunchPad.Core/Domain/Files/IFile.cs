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

namespace DeploySoftware.LaunchPad.Core.Domain
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Marks any object as a file that can be manipulated by the platform.
    /// Each file is uniquely identified by its FileKey.
    /// </summary>
    public interface IFile<TPrimaryKey, TFileContentType> : ILaunchPadObject
    {

        /// <summary>
        /// The size of the file, in bytes
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public Int64 Size { get; set; }

        /// <summary>
        /// The name of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name { get; set; }

        /// <summary>
        /// The extension of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String Extension { get; set; }

        /// <summary>
        /// The content / mime type of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String MimeType { get; set; }


        /// <summary>
        /// The content of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public TFileContentType Content { get; set; }

    }
}
