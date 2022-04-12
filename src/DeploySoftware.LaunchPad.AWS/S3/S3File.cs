using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public partial class S3File<TIdType, TFileContentType> : FileBase<TIdType, TFileContentType, S3BucketStorageLocation>
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
                return Location.BasePrefix + "/" + Name;
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
                return new Uri("https://" + Location.BucketName + ".s3.amazonaws.com/" + Location.BasePrefix + "/" + Name.Replace(" ","+"));
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
                return new Uri("https://s3." + Location.Region + ".amazonaws.com/" + Location.BucketName + "/" + Location.BasePrefix + "/" + Name.Replace(" ", "+"));
            }
        }

        public S3File() : base()
        {

        }

    }
}
