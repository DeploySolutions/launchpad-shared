﻿//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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

using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public partial class S3BucketStorageLocation : FileStorageLocationBase
    {
        public const string DEFAULT_REGION = "us-east-1";
        public const string DEFAULT_BUCKET_ROOT = "https://s3-us-east-1.amazonaws.com";
        
        [DataObjectField(false)]
        [XmlAttribute]
        public string Region { get; set; }
        
        [DataObjectField(false)]
        [XmlAttribute]
        public string BucketName { get; set; }


        [DataObjectField(false)]
        [XmlAttribute]
        public string Prefix { get; set; }


        /// <summary>
        /// Creates a new bucket location object with the default region and bucket root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        public S3BucketStorageLocation()
        {
            Region = DEFAULT_REGION;
            RootPath = new Uri(DEFAULT_BUCKET_ROOT);
        }

        /// <summary>
        /// Create a  new bucket location object with the given bucketname, in the default region and root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="bucketName">The globally-unique name of the bucket.</param>
        public S3BucketStorageLocation(string bucketName) : base()
        {
            Region = DEFAULT_REGION;
            RootPath = new Uri(DEFAULT_BUCKET_ROOT);
            BucketName = bucketName;
        }

        /// <summary>
        /// Creates a new bucket location with the given region, bucket root, and bucketname.
        /// Note that the provided bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="region">The region in which the bucket will be created.</param>
        /// <param name="bucketRoot">the URI root of the bucket</param>
        /// <param name="bucketName">The globally-unique name of the bucket</param>
        public S3BucketStorageLocation(string region, Uri bucketRoot, string bucketName) : base()
        {
            Region = region;
            RootPath = new Uri("https://s3-" + bucketRoot + ".amazonaws.com");
            BucketName = bucketName;
        }


        /// <summary>
        /// Creates a new bucket location with the given region, bucket root, and bucketname.
        /// Note that the provided bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="region">The region in which the bucket will be created.</param>
        /// <param name="bucketRoot">the URI root of the bucket</param>
        /// <param name="bucketName">The globally-unique name of the bucket</param>
        public S3BucketStorageLocation(string region, Uri bucketRoot, string bucketName, string prefix) : base()
        {
            Region = region;
            RootPath = new Uri("https://s3-" + bucketRoot + ".amazonaws.com");
            BucketName = bucketName;
            Prefix = prefix;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected S3BucketStorageLocation(SerializationInfo info, StreamingContext context) :base(info,context)
        {
            Region = info.GetString(Region);
            BucketName = info.GetString(BucketName);
            Prefix = info.GetString(Prefix); 
            Data = (byte[])info.GetValue("Data", typeof(byte[]));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Region", Region);
            info.AddValue("BucketName", BucketName);
            info.AddValue("Data", Data);
            info.AddValue("Prefix", Prefix);
        }

        /// <summary>
        /// Returns a Uri to the complete filepath, for a given filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override Uri GetFullPathUri(string fileName)
        {
            return new Uri(RootPath.AbsolutePath + "/" + Prefix + "/" + fileName);
        }
    }
}