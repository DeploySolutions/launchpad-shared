using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public partial class S3File<TIdType> : FileBase<TIdType, S3BucketStorageLocation>
    {
        /// <summary>
        /// The full path of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String ObjectKey
        {
            get
            {
                return Location.Prefix + "/" + Name.Replace(" ", "+");
            }
        }

        /// <summary>
        /// The full path of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri VirtualPathUri
        {
            get
            {
                return new Uri("https://" + Location.BucketName + ".s3.amazonaws.com/" + Location.Prefix + "/" + Name.Replace(" ","+"));
            }
        }

        /// <summary>
        /// The full path of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public override Uri FullPathUri
        {
            get
            {
                return new Uri("https://s3." + Location.Region + ".amazonaws.com/" + Location.BucketName + "/" + Location.Prefix + "/" + Name.Replace(" ", "+"));
            }
        }

        public S3File() : base()
        {

        }

    }
}
