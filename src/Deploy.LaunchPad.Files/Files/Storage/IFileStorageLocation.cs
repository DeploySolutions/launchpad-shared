// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IFileStorageLocation.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Files.Storage
{
    using Deploy.LaunchPad.Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    /// <summary>
    /// The storage location of the file bytes.
    /// </summary>
    public partial interface IFileStorageLocation : ILaunchPadObject, ILaunchPadMinimalProperties,
        IComparable<GenericFileStorageLocation>, IEquatable<GenericFileStorageLocation>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the root URI.
        /// </summary>
        /// <value>The root URI.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public Uri RootUri { get; set; }


        /// <summary>
        /// Gets or sets the default prefix.
        /// </summary>
        /// <value>The default prefix.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string DefaultPrefix { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public FileStorageLocationTypeEnum Provider { get; set; }


        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileToCheck">The file to check.</param>
        /// <param name="shouldRecurseSubdirectories">if set to <c>true</c> [should recurse subdirectories].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool FileExists<TFile, TFileContentType,TSchemaFormat>(TFile fileToCheck, bool shouldRecurseSubdirectories = false)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="fileTags">The file tags.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="writeTags">The write tags.</param>
        /// <param name="filePrefix">The file prefix.</param>
        /// <param name="fileSuffix">The file suffix.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CreateFile<TFile, TFileContentType, TSchemaFormat>(TFile sourceFile, IDictionary<string, string> fileTags, string contentType, IDictionary<string, string> writeTags, string filePrefix, string fileSuffix)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Creates the file asynchronous.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="fileTags">The file tags.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="writeTags">The write tags.</param>
        /// <param name="filePrefix">The file prefix.</param>
        /// <param name="fileSuffix">The file suffix.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> CreateFileAsync<TFile, TFileContentType, TSchemaFormat>(TFile sourceFile, IDictionary<string, string> fileTags, string contentType, IDictionary<string, string> writeTags, string filePrefix, string fileSuffix)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="tempLocation">The temporary location.</param>
        /// <returns>TFile.</returns>
        public TFile ReadFile<TFile, TFileContentType, TSchemaFormat>(string fileId, Uri tempLocation = null)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Reads the file asynchronous.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="tempLocation">The temporary location.</param>
        /// <returns>Task&lt;TFile&gt;.</returns>
        public Task<TFile> ReadFileAsync<TFile, TFileContentType, TSchemaFormat>(string fileId, Uri tempLocation = null)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Reads the file metadata.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="sourceFile">The source file.</param>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        public IDictionary<string, string> ReadFileMetadata<TFile, TFileContentType, TSchemaFormat>(TFile sourceFile)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Reads the file metadata asynchronous.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="sourceFile">The source file.</param>
        /// <returns>Task&lt;IDictionary&lt;System.String, System.String&gt;&gt;.</returns>
        public Task<IDictionary<string, string>> ReadFileMetadataAsync<TFile, TFileContentType, TSchemaFormat>(TFile sourceFile)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileToUpdate">The file to update.</param>
        /// <returns>TFile.</returns>
        public TFile UpdateFile<TFile, TFileContentType, TSchemaFormat>(TFile fileToUpdate)
            where TFile : IFile<TFileContentType,TSchemaFormat>, new();

        /// <summary>
        /// Updates the file asynchronous.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileToUpdate">The file to update.</param>
        /// <returns>Task&lt;TFile&gt;.</returns>
        public Task<TFile> UpdateFileAsync<TFile, TFileContentType, TSchemaFormat>(TFile fileToUpdate)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileToDelete">The file to delete.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DeleteFile<TFile, TFileContentType, TSchemaFormat>(TFile fileToDelete)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Deletes the file asynchronous.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="fileToDelete">The file to delete.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> DeleteFileAsync<TFile, TFileContentType, TSchemaFormat>(TFile fileToDelete)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Gets the relative path for file.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns>Uri.</returns>
        public Uri GetRelativePathForFile<TFile, TFileContentType, TSchemaFormat>(TFile file)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Gets the full path for file.
        /// </summary>
        /// <typeparam name="TFile">The type of the t file.</typeparam>
        /// <typeparam name="TFileId">The type of the t file identifier.</typeparam>
        /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns>Uri.</returns>
        public Uri GetFullPathForFile<TFile, TFileContentType, TSchemaFormat>(TFile file)
            where TFile : IFile<TFileContentType, TSchemaFormat>, new();

        /// <summary>
        /// Returns available storage space for this location, in bytes, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long GetAvailableStorageSpaceInBytes();


        /// <summary>
        /// Returns available storage space for this location, in GB, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns>System.Double.</returns>
        public double GetAvailableStorageSpaceInGigabytes();

    }
}
