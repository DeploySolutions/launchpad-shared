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

using Amazon.S3.Model;
using Amazon.S3;
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.Abp.S3.Services;
using Deploy.LaunchPad.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public virtual AwsS3Service S3Service { get; private set; }

        /// <summary>
        /// Creates a new bucket location object with the default region and bucket root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        public S3BucketStorageLocation() : base()
        {
            Id = Guid.NewGuid().ToString();
            Name = Id;
            Region = DEFAULT_REGION;
            S3Service = new AwsS3Service(Logger, Region);
            string bucketUri = string.Format("https://s3.{0}.amazonaws.com/{1}", Region, Id);
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
        public S3BucketStorageLocation(ILogger logger, string id, string bucketName, string region, string defaultPrefix = "") : base(logger)
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
        /// Create a  new bucket location object with the given bucketname, in the default region and root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="bucketName">The globally-unique name of the bucket.</param>
        public S3BucketStorageLocation(ILogger logger, string id, Uri rootUri) : base(logger, id, rootUri)
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
        public override Uri GetRelativePathForFile<TFile, TFileId, TFileContentType>(TFile file)
        {
            return new Uri("/" + DefaultPrefix + "/" + file.Name.Replace(" ", "+"));
        }

        /// <summary>
        /// The full path of the file
        /// </summary>
        public override Uri GetFullPathForFile<TFile, TFileId, TFileContentType>(TFile file)
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

        public override bool FileExists<TFile, TFileId, TFileContentType>(TFile fileToCheck, bool shouldRecurseSubdirectories = false)
        {
            return S3Service.CheckIfFileExists(Name, fileToCheck.Id.ToString()).Result;
        }


        public override async Task<TFile> ReadFileAsync<TFile, TFileId, TFileContentType>(string fileId, Uri tempLocation = null)
        {
            var file = new TFile();
            bool succeeded = false;
            if (tempLocation!= null && tempLocation.IsUnc)
            {
                succeeded = await S3Service.DownloadFileFromBucketToLocalviaTransferUtilityAsync(Name, fileId, tempLocation.AbsolutePath, null, null);

            }
            else
            {
                succeeded = S3Service.GetFileFromBucketAsync(Name, fileId).IsCompletedSuccessfully;

            }
            return file;
        }

        public override async Task<bool> CreateFileAsync<TFile, TFileId, TFileContentType>(TFile sourceFile, IDictionary<string, string> fileTags, string contentType, IDictionary<string, string> writeTags, string filePrefix, string fileSuffix)
        {
            bool succeeded = await S3Service.UploadLocalFileToBucketviaTransferUtilityAsync(Name,sourceFile.Name, @"c:\temp\",fileTags,filePrefix,contentType,writeTags,S3StorageClass.Standard);
            return succeeded;

        }


    }
}
