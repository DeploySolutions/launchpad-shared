using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public class S3File<TIdType> : FileBase<TIdType, S3BucketStorageLocation>
    {

        /// <summary>
        /// The full path of the file
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public override Uri FullPathUri
        {
            get
            {
                return new Uri("https://" + Location.BucketName + "-files.s3.amazonaws.com/" + Location.Prefix + "/" + Name);
            }
        }

        public S3File() : base()
        {

        }

    }
}
