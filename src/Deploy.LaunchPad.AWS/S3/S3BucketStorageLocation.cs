// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-23-2023
// ***********************************************************************
// <copyright file="S3BucketStorageLocation.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

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

using Amazon.S3;
using Amazon.S3.Model;
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.S3.Services;
using Deploy.LaunchPad.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Deploy.LaunchPad.Files.Storage;
using Deploy.LaunchPad.Files;
using Deploy.LaunchPad.Core.Elements;

namespace Deploy.LaunchPad.AWS.S3
{
    /// <summary>
    /// Class S3BucketStorageLocation.
    /// Implements the <see cref="GenericFileStorageLocation" />
    /// </summary>
    /// <seealso cref="GenericFileStorageLocation" />
    [Owned]
    public partial class S3BucketStorageLocation : GenericFileStorageLocation
    {
        /// <summary>
        /// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        /// </summary>
        /// <value>The debug display.</value>
        protected override string _debugDisplay => $"{Id}. Name {Name}.";

        /// <summary>
        /// The default region
        /// </summary>
        public const string DEFAULT_REGION = "us-east-1";

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>The region.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Region { get; set; }

        /// <summary>
        /// Gets the s3 service.
        /// </summary>
        /// <value>The s3 service.</value>
        public virtual AwsS3Service S3Service { get; private set; }

        /// <summary>
        /// Creates a new bucket location object with the default region and bucket root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        public S3BucketStorageLocation() : base()
        {
            Id = Guid.NewGuid().ToString();
            Name = new ElementName(Id);
            Region = DEFAULT_REGION;
            S3Service = new AwsS3Service(Logger, Region);
            string bucketUri = string.Format("https://s3.{0}.amazonaws.com/{1}", Region, Id);
            string descriptionMessage = string.Format("AWS S3 bucket at '{0}'", bucketUri);
            Description = new ElementDescription( descriptionMessage);
            RootUri = new Uri(bucketUri);
            Provider = FileStorageLocationTypeEnum.Aws_S3;
        }

        /// <summary>
        /// Creates a new bucket location with the given region, bucket root, and bucketname.
        /// Note that the provided bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="bucketName">The globally-unique name of the bucket</param>
        /// <param name="region">The region in which the bucket will be created.</param>
        /// <param name="defaultPrefix">The default prefix.</param>
        public S3BucketStorageLocation(ILogger logger, string id, string bucketName, string region, string defaultPrefix = "") : base(logger)
        {
            Region = region;
            Name = new ElementName(bucketName);
            string bucketUri = string.Format("https://s3.{0}.amazonaws.com/{1}", Region, bucketName);
            string descriptionMessage = string.Format("AWS S3 bucket at '{0}'", bucketUri);
            Description = new ElementDescription(descriptionMessage);
            DefaultPrefix = defaultPrefix;
            RootUri = new Uri(bucketUri);
        }

        /// <summary>
        /// Create a  new bucket location object with the given bucketname, in the default region and root.
        /// Note that the bucket may not be globally unique and this constructor does not check that.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="rootUri">The root URI.</param>
        public S3BucketStorageLocation(ILogger logger, string id, Uri rootUri) : base(logger, rootUri, id)
        {
            Region = DEFAULT_REGION;
            string bucketUri = string.Format("https://s3.{0}.amazonaws.com/{1}", Region, id);
            string descriptionMessage = string.Format("AWS S3 bucket at '{0}'", bucketUri);
            Description = new ElementDescription(descriptionMessage);
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

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
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
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns>Uri.</returns>
        public override Uri GetRelativePathForFile<TFile, TFileContentType, TFileSchema>(TFile file)
        {
            return new Uri("/" + DefaultPrefix + "/" + file.Name.Full.Replace(" ", "+"));
        }

        /// <summary>
        /// The full path of the file
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns>Uri.</returns>
        public override Uri GetFullPathForFile<TFile, TFileContentType, TFileSchema>(TFile file)
        {
            return new Uri("https://s3." + Region + ".amazonaws.com/" + Name + "/" + DefaultPrefix + "/" + file.Name.Full.Replace(" ", "+"));
        }

        /// <summary>
        /// The full path of the file
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns>String.</returns>
        public virtual String GetObjectKeyForFile<TPrimaryKey, TFileContentType, TFileSchema>(IFile<TFileContentType, TFileSchema> file)
        {
            return DefaultPrefix + "/" + file.Name;
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileToCheck">The file to check.</param>
        /// <param name="shouldRecurseSubdirectories">if set to <c>true</c> [should recurse subdirectories].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool FileExists<TFile, TFileContentType, TFileSchema>(TFile fileToCheck, bool shouldRecurseSubdirectories = false)
        {
            return S3Service.CheckIfFileExists(Name.Full, fileToCheck.Name.Full.ToString()).Result;
        }

        /// <summary>
        /// Read file as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="tempLocation">The temporary location.</param>
        /// <returns>A Task&lt;TFile&gt; representing the asynchronous operation.</returns>
        public override async Task<TFile> ReadFileAsync<TFile, TFileContentType, TFileSchema>(string fileId, Uri tempLocation = null)
        {
            var file = new TFile();
            bool succeeded = false;
            if (tempLocation!= null && tempLocation.IsUnc)
            {
                succeeded = await S3Service.DownloadFileFromBucketToLocalviaTransferUtilityAsync(Name.Full, fileId, tempLocation.AbsolutePath, null, null);

            }
            else
            {
                succeeded = S3Service.GetFileFromBucketAsync(Name.Full, fileId).IsCompletedSuccessfully;

            }
            return file;
        }

        /// <summary>
        /// Create file as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TFile">The type of the file.</typeparam>
        /// <typeparam name="TFileContentType">The type of the file content type.</typeparam>
        /// <typeparam name="TFileSchema">The type of the file schema.</typeparam>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="fileTags">The file tags.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="writeTags">The write tags.</param>
        /// <param name="filePrefix">The file prefix.</param>
        /// <param name="fileSuffix">The file suffix.</param>
        /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
        public override async Task<bool> CreateFileAsync<TFile, TFileContentType, TFileSchema>(
            TFile sourceFile,
            IDictionary<string, string> fileTags,
            string contentType,
            IDictionary<string, string> writeTags,
            string filePrefix,
            string fileSuffix)
        {
            // Generate a secure random temporary file path
            var randomPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            try
            {
                // Create a secure temporary file with write, non-inheritable permissions
                using (var fileStream = new FileStream(
                    randomPath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    4096,
                    FileOptions.DeleteOnClose))
                {
                    // Write the source file content to the temporary file
                    using (var writer = new StreamWriter(fileStream))
                    {
                        await writer.WriteAsync(sourceFile.Name.Full); // Assuming sourceFile.Name.Full contains the file content
                    }
                }

                // Upload the temporary file to the S3 bucket
                bool succeeded = await S3Service.UploadLocalFileToBucketviaTransferUtilityAsync(
                    Name.Full,
                    sourceFile.Name.Full,
                    randomPath,
                    fileTags,
                    filePrefix,
                    contentType,
                    writeTags,
                    S3StorageClass.Standard);

                return succeeded;
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred while creating the file.", ex);
                throw;
            }
            finally
            {
                // Ensure the temporary file is deleted if it still exists
                if (File.Exists(randomPath))
                {
                    File.Delete(randomPath);
                }
            }
        }

    }
}
