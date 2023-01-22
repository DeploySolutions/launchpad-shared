//LaunchPad Shared
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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

using Deploy.LaunchPad.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.AWS.S3
{
    [Owned]
    public partial class S3BucketStorageLocation : GenericFileStorageLocation
    {
        /// <summary>
        /// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        /// </summary>
        protected override string _debugDisplay => $"{Id}. Name {Name}.";

        public const string DEFAULT_REGION = "us-east-1";

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Region { get; set; }

        /// <summary>
        /// Creates a new bucket location object with the default region and bucket root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        public S3BucketStorageLocation() : base()
        {
            Id = Guid.NewGuid().ToString();
            Name = Id;
            Region = DEFAULT_REGION;
            string bucketUri = string.Format("https://s3.{0}.amazonaws.com/{1}", Region, Id);
            string descriptionMessage = string.Format("AWS S3 bucket at '{0}'", bucketUri);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            RootUri = new Uri(bucketUri);
            Provider = FileStorageLocationTypeEnum.Aws_S3;
        }

        /// <summary>
        /// Create a  new bucket location object with the given bucketname, in the default region and root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="bucketName">The globally-unique name of the bucket.</param>
        public S3BucketStorageLocation(string id, Uri rootUri) : base(id, rootUri)
        {
            Region = DEFAULT_REGION;
            string bucketUri = string.Format("https://s3.{0}.amazonaws.com/{1}", Region, id);
            string descriptionMessage = string.Format("AWS S3 bucket at '{0}'", bucketUri);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            RootUri = new Uri(bucketUri);
            Provider = FileStorageLocationTypeEnum.Aws_S3;
        }

        /// <summary>
        /// Creates a new bucket location with the given region, bucket root, and bucketname.
        /// Note that the provided bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="region">The region in which the bucket will be created.</param>
        /// <param name="bucketRoot">the URI root of the bucket</param>
        /// <param name="bucketName">The globally-unique name of the bucket</param>
        public S3BucketStorageLocation(string id, string bucketName, string region, string defaultPrefix = "") : base()
        {
            Region = region;
            Name = bucketName;
            string bucketUri = string.Format("https://s3.{0}.amazonaws.com/{1}", Region, bucketName);
            string descriptionMessage = string.Format("AWS S3 bucket at '{0}'", bucketUri);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            DefaultPrefix = defaultPrefix;
            RootUri = new Uri(bucketUri);
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected S3BucketStorageLocation(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Region = info.GetString(Region);
            DefaultPrefix = info.GetString(DefaultPrefix);

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Region", Region);
            info.AddValue("BasePrefix", DefaultPrefix);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[S3BucketStorageLocation: ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// The virtual path of the file
        /// </summary>
        public override Uri GetRelativePathForFile<TFilePrimaryKey, TFileContentType>(IFile<TFilePrimaryKey, TFileContentType> file)
        {
            return new Uri("/" + DefaultPrefix + "/" + file.Name.Replace(" ", "+"));
        }

        /// <summary>
        /// The full path of the file
        /// </summary>
        public override Uri GetFullPathForFile<TFilePrimaryKey, TFileContentType>(IFile<TFilePrimaryKey, TFileContentType> file)
        {
            return new Uri("https://s3." + Region + ".amazonaws.com/" + Name + "/" + DefaultPrefix + "/" + file.Name.Replace(" ", "+"));
        }

        /// <summary>
        /// The full path of the file
        /// </summary>
        public virtual String GetObjectKeyForFile<TPrimaryKey, TFileContentType>(IFile<TPrimaryKey, TFileContentType> file)
        {
            return DefaultPrefix + "/" + file.Name;
        }


    }
}
