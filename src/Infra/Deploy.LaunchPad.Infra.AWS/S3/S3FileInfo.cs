using Deploy.LaunchPad.Files;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Infra.AWS.S3
{
    public partial class S3FileInfo : IMustHaveFullName, IS3FileInfo
    {
        /// <summary>
        /// A Full Name for this entity
        /// </summary>
        /// <value>The Full Name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        //[JsonPropertyName("description")]
        public virtual string Name { get; set; }

        public IFileContent<object> Content { get; set; }
        public long? FileSize { get; set; }
        public S3FileInfo()
        {
        }
    }
}
