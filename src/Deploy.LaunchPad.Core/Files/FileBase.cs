using Deploy.LaunchPad.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Deploy.LaunchPad.Core.Files.Storage;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Core.Files
{
    public abstract partial class FileBase<TFileContentType> : LaunchPadCommonProperties, IFile<TFileContentType>
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
        /// The size of the file, in bytes
        /// </summary>
        /// <value>The size.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IList<IFileStorageLocation> Locations { get; set; } = new List<IFileStorageLocation>();

        /// <summary>
        /// The schema of the file
        /// </summary>
        /// <value>The content.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ILaunchPadSchemaDetails Schema { get; set; }


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
            Locations = (IList<IFileStorageLocation>)info.GetValue("Locations", typeof(IFileStorageLocation));
            Content = (TFileContentType)info.GetValue("Content", typeof(TFileContentType));
            Schema = (ILaunchPadSchemaDetails)info.GetValue("Schema", typeof(ILaunchPadSchemaDetails));
            Size = info.GetInt64("Size");
            MimeType = info.GetString("MimeType");
            Extension = info.GetString("Extension");
            Encoding = info.GetString("Encoding");
        }


        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.DeserializeCommonProperties(info, context);
            info.AddValue("Locations", Locations);
            info.AddValue("Size", Size);
            info.AddValue("MimeType", MimeType);
            info.AddValue("Extension", Extension);
            info.AddValue("Encoding", Encoding);
            info.AddValue("Content", Content);
            info.AddValue("Schema", Schema);
        }
    }
}
