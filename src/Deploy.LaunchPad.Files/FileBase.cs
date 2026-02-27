using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Files.Formats;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Deploy.LaunchPad.Files
{
    /// <summary>
    /// Represents a file with a specific content type and schema format.
    /// </summary>
    /// <typeparam name="TFileContentType">The type of the content which will be stored within.</typeparam>
    /// <typeparam name="TSchemaFormat">The format of the file schema, used to validate it or ensure output is correct.</typeparam>
    public abstract partial class FileBase<TFileContentType, TSchemaFormat> : LaunchPadMinimalProperties, IFile<TFileContentType, TSchemaFormat>
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
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the last modification time.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime? LastModificationTime { get; set; }


        /// <summary>
        /// The schema of the file
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ILaunchPadSchemaDetails<TSchemaFormat>? Schema { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected FileBase(string fileName) : base(fileName)
        {
            Name = new ElementName(fileName);
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected FileBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Content = (TFileContentType)info.GetValue("Content", typeof(TFileContentType));
            Schema = (ILaunchPadSchemaDetails<TSchemaFormat>)info.GetValue("Schema", typeof(ILaunchPadSchemaDetails<TSchemaFormat>));
            Size = info.GetInt64("Size");
            MimeType = info.GetString("MimeType");
            Extension = info.GetString("Extension");
            Encoding = info.GetString("Encoding");
        }


        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
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
