// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="FileBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

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

using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Core.Util;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Class FileBase.
    /// Implements the <see cref="LaunchPadDomainEntityBase{TIdType}" />
    /// Implements the <see cref="IFile{TIdType, TFileContentType}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    /// <seealso cref="LaunchPadDomainEntityBase{TIdType}" />
    /// <seealso cref="IFile{TIdType, TFileContentType}" />
    public abstract partial class FileBase<TIdType, TFileContentType> : LaunchPadDomainEntityBase<TIdType>,
        IFile<TIdType, TFileContentType>
    {

        /// <summary>
        /// The size of the file, in bytes
        /// </summary>
        /// <value>The size.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual long Size { get; set; }

        /// <summary>
        /// The extension of the file
        /// </summary>
        /// <value>The extension.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Extension { get; set; }

        /// <summary>
        /// The content / mime type of the file
        /// </summary>
        /// <value>The type of the MIME.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        public virtual String MimeType { get; set; }


        /// <summary>
        /// The content of the file. May be null (for instance, if not loaded or populated yet)
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        public virtual TFileContentType Content { get; set; }

        /// <summary>
        /// Properties and methods for the file's content hash (to facilitate file verification)
        /// </summary>
        /// <value>The checksum.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Checksum Checksum { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FileBase{TIdType, TFileContentType}"/> class.
        /// </summary>
        protected FileBase()
        {
            Checksum = new Checksum();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBase{TIdType, TFileContentType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected FileBase(TIdType id) : base(id)
        {
            Id = id;
            Checksum = new Checksum();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileBase{TIdType, TFileContentType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        protected FileBase(string fileName) : base()
        {
            Name = new ElementName(fileName, fileName);
            Description = new ElementDescription(string.Empty, string.Empty);
            Checksum = new Checksum();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBase{TIdType, TFileContentType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        protected FileBase(TIdType id, string fileName) : base()
        {
            Id = id;
            Name = new ElementName(fileName, fileName);
            Checksum = new Checksum();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FileBase{TIdType, TFileContentType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        protected FileBase(TIdType id, string fileName, TFileContentType content) : base()
        {
            Id = id;
            Name = new ElementName(fileName, fileName);
            Content = content;
            Checksum = new Checksum();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Content = (TFileContentType)info.GetValue("Content", typeof(TFileContentType));
            Checksum = (Checksum)info.GetValue("Checksum", typeof(Checksum));
            Size = info.GetInt64("Size");
            MimeType = info.GetString("MimeType");
            Extension = info.GetString("Extension");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Size", Size);
            info.AddValue("Name", Name);
            info.AddValue("MimeType", MimeType);
            info.AddValue("Extension", Extension);
            info.AddValue("Content", Content);
            info.AddValue("Checksum", Checksum);
        }


    }
}
