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

using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Util.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.Files
{
    /// <summary>
    /// Class FileBase.
    /// Implements the <see cref="DomainEntityBase{TPrimaryKey}" />
    /// Implements the <see cref="IDomainEntityFile{TPrimaryKey, TFileContentType}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t identifier type.</typeparam>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    /// <seealso cref="DomainEntityBase{TPrimaryKey}" />
    /// <seealso cref="IDomainEntityFile{TPrimaryKey, TFileContentType}" />
    public abstract partial class DomainEntityFileBase<TPrimaryKey, TFileContentType, TSchemaFormat> : DomainEntityBase<TPrimaryKey>,
        IDomainEntityFile<TPrimaryKey, TFileContentType, TSchemaFormat>
    {
        /// <summary>
        /// The Name of the file
        /// </summary>
        /// <value>The Name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; set; }


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
        /// The encoding of the file
        /// </summary>
        /// <value>The encoding.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Encoding { get; set; } = "UTF-8";

        /// <summary>
        /// The content / mime type of the file
        /// </summary>
        /// <value>The type of the MIME.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        public virtual string MimeType { get; set; }


        /// <summary>
        /// The content of the file. May be null (for instance, if not loaded or populated yet)
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        public virtual TFileContentType Content { get; set; }

        /// <summary>
        /// The schema of the file
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ILaunchPadSchemaDetails<TSchemaFormat> Schema { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityFileBase{TPrimaryKey, TFileContentType}"/> class.
        /// </summary>
        protected DomainEntityFileBase()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityFileBase{TPrimaryKey, TFileContentType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected DomainEntityFileBase(TPrimaryKey id) : base(id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityFileBase{TPrimaryKey, TFileContentType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        protected DomainEntityFileBase(string fileName) : base()
        {
            Name = fileName;
            //Description = new ElementDescription(string.Empty, string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityFileBase{TPrimaryKey, TFileContentType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        protected DomainEntityFileBase(TPrimaryKey id, string fileName) : base()
        {
            Id = id;
            Name = fileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityFileBase{TPrimaryKey, TFileContentType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        protected DomainEntityFileBase(TPrimaryKey id, string fileName, TFileContentType content) : base()
        {
            Id = id;
            Name = fileName;
            Content = content;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DomainEntityFileBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Content = (TFileContentType)info.GetValue("Content", typeof(TFileContentType));
            Schema = (ILaunchPadSchemaDetails<TSchemaFormat>)info.GetValue("Schema", typeof(ILaunchPadSchemaDetails<TSchemaFormat>));
            Size = info.GetInt64("Size");
            MimeType = info.GetString("MimeType");
            Extension = info.GetString("Extension");
            Encoding = info.GetString("Encoding");
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
            info.AddValue("MimeType", MimeType);
            info.AddValue("Extension", Extension);
            info.AddValue("Encoding", Encoding);
            info.AddValue("Content", Content);
            info.AddValue("Schema", Schema);
        }


    }
}
