using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Util.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Infra.AWS.S3
{
    public partial class S3BucketInfo : IMustHaveFullName, IS3BucketInfo
    {

        /// <summary>
        /// A Full Name for this entity
        /// </summary>
        /// <value>The Full Name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        //[JsonPropertyName("description")]
        public virtual string Name { get; set; }

        public S3BucketInfo(string bucketName)
        {
            Name = bucketName;
            //Description = new ElementDescription(bucketName);
        }
    }
}
