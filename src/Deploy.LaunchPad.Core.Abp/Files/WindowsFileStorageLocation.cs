// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-26-2023
// ***********************************************************************
// <copyright file="WindowsFileStorageLocation.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
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

using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Values;
using DocumentFormat.OpenXml.Packaging;
using Deploy.LaunchPad.Core.Files;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Class WindowsFileStorageLocation.
    /// Implements the <see cref="GenericFileStorageLocation" />
    /// </summary>
    /// <seealso cref="GenericFileStorageLocation" />
    [Owned]
    public partial class WindowsFileStorageLocation : GenericFileStorageLocation
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFileStorageLocation"/> class.
        /// </summary>
        public WindowsFileStorageLocation() : base()
        {
            string defaultUri = string.Format("file:///{0}", Directory.GetCurrentDirectory());
            FileInfo file = new FileInfo(defaultUri);
            DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);
            string driveRoot = drive.RootDirectory.FullName;
            string descriptionMessage = string.Format("Windows file share drive '{0}'", driveRoot);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            RootUri = new Uri(defaultUri);
            Provider = FileStorageLocationTypeEnum.Windows_NTFS;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFileStorageLocation"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="rootUri">The root URI.</param>
        public WindowsFileStorageLocation(ILogger logger, string id, Uri rootUri) : base(logger, id, rootUri)
        {
            FileInfo file = new FileInfo(RootUri.ToString());
            DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);
            string driveRoot = drive.RootDirectory.FullName;
            string descriptionMessage = string.Format("Windows file share drive '{0}'", driveRoot);
            DescriptionShort = descriptionMessage;
            DescriptionFull = descriptionMessage;
            Provider = FileStorageLocationTypeEnum.Windows_NTFS;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected WindowsFileStorageLocation(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[WindowsFileStorageLocation: ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }


        /// <summary>
        /// Returns available storage space for this location, in bytes, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns>System.Int64.</returns>
        public override long GetAvailableStorageSpaceInBytes()
        {
            DriveInfo drive = new DriveInfo(RootUri.LocalPath);
            if (drive.IsReady)
            {
                return drive.AvailableFreeSpace;
            }
            return -1;
        }

        /// <summary>
        /// Returns available storage space for this location, in GB, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns>System.Double.</returns>
        public override double GetAvailableStorageSpaceInGigabytes()
        {
            long driveSpace = GetAvailableStorageSpaceInBytes();
            if (driveSpace > 0)
            {
                return driveSpace / (1024 * 1024 * 1024);
            }
            return -1;
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
        public override bool FileExists<TFile, TFileContentType>(TFile fileToCheck, bool shouldRecurseSubdirectories = false)
        {
            bool doesFileExist = false;
            DirectoryInfo di = new DirectoryInfo(RootUri.AbsolutePath);
            if (di.Exists)
            {
                string searchPattern = fileToCheck.Name + "." + fileToCheck.Extension;
                try
                {
                    EnumerationOptions options = new EnumerationOptions();
                    options.RecurseSubdirectories = shouldRecurseSubdirectories;
                    var output = Directory.EnumerateFiles(RootUri.AbsolutePath, searchPattern, options).FirstOrDefault();
                    if (output != null)
                    {
                        doesFileExist = true;
                    }
                }
                catch (UnauthorizedAccessException uAEx)
                {
                    Logger.Error(uAEx.Message);
                }
                catch (PathTooLongException pathEx)
                {
                    Logger.Error(pathEx.Message);
                }

            }
            return doesFileExist;
        }

        /// <summary>
        /// Read file metadata as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="sourceFile">The source file.</param>
        /// <returns>A Task&lt;IDictionary`2&gt; representing the asynchronous operation.</returns>
        public override async Task<IDictionary<string, string>> ReadFileMetadataAsync<TFile, TFileContentType>(TFile sourceFile)
        {
            IDictionary<string, string> metadata = new Dictionary<string, string>();
            DirectoryInfo di = new DirectoryInfo(RootUri.AbsolutePath);
            if (di.Exists)
            {
                string searchPattern = sourceFile.Name + "." + sourceFile.Extension;
                try
                {
                    EnumerationOptions options = new EnumerationOptions();
                    options.RecurseSubdirectories = true;
                    var output = Directory.EnumerateFiles(RootUri.AbsolutePath, searchPattern, options).FirstOrDefault();
                    if (output != null)
                    {
                        /**
                         * Some common properties (Windows file share and Office)
                            Owner
                            Size
                            Date created
                            Date modified
                            Date accessed
                            Content type
                            Title
                            Subject
                            Tags
                            Categories
                            Comments
                            Authors
                            Last saved by
                            Revision number
                            Version number
                            Program name
                            Company
                            Manager
                            Word count
                            Paragraph count
                            Line count
                            Template
                         * 
                         */
                        var file = new FileInfo(output);

                        //// dateCreated 
                        var dateCreated = file.CreationTimeUtc;
                        metadata.TryAdd("CreationTimeUtc", dateCreated.ToString());

                        var dateLastModified = file.LastWriteTimeUtc;
                        metadata.TryAdd("LastWriteTimeUtc", dateLastModified.ToString());

                        //size
                        var size = file.Length;
                        metadata.TryAdd("Size", size.ToString());
                        
                        // if it's word
                        if (sourceFile.Extension == "docx")
                        {
                            // use office power tools
                            using (WordprocessingDocument wDoc = WordprocessingDocument.Open(output, true))
                            {
                                var extendedProperties = wDoc.ExtendedFilePropertiesPart;
                                if (extendedProperties != null)
                                {
                                    metadata.TryAdd("Lines",extendedProperties.Properties.Lines.InnerText);
                                    metadata.TryAdd("Words", extendedProperties.Properties.Words.InnerText);
                                    metadata.TryAdd("Paragraphs", extendedProperties.Properties.Paragraphs.InnerText);
                                    metadata.TryAdd("Application", extendedProperties.Properties.Application.InnerText);
                                    metadata.TryAdd("Application Version", extendedProperties.Properties.ApplicationVersion.InnerText);

                                }
                            }
                        }
                    }
                }
                catch (AccessViolationException avEx)
                {
                    Logger.Error(avEx.Message);
                }
                catch (UnauthorizedAccessException uAEx)
                {
                    Logger.Error(uAEx.Message);
                }
                catch (PathTooLongException pathEx)
                {
                    Logger.Error(pathEx.Message);
                }
                catch(Exception ex)
                {
                    Logger.Equals(ex.Message);
                }
            }
            return metadata;
        }

    }
}
