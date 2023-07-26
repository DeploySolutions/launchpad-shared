﻿//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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

namespace Deploy.LaunchPad.Core.Domain
{
    using Deploy.LaunchPad.Core.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    /// <summary>
    /// The storage location of the file bytes.
    /// </summary>
    public interface IFileStorageLocation : ILaunchPadObject,
        IComparable<GenericFileStorageLocation>, IEquatable<GenericFileStorageLocation>
    {
        [DataObjectField(true)]
        [XmlAttribute]
        public string Id { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// A short description for this storage location
        /// </summary>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this storage location
        /// </summary>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public string? DescriptionFull { get; set; }

        [DataObjectField(true)]
        [XmlAttribute]
        public bool IsReadOnly { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public Uri RootUri { get; set; }


        [DataObjectField(false)]
        [XmlAttribute]
        public string DefaultPrefix { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public FileStorageLocationTypeEnum Provider { get; set; }

        /// <summary>
        /// The location have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<MetadataTag> Tags { get; set; }


        public bool FileExists<TFile, TFileId, TFileContentType>(TFile fileToCheck, bool shouldRecurseSubdirectories = false)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public bool CreateFile<TFile, TFileId, TFileContentType>(TFile sourceFile, IDictionary<string, string> fileTags, string contentType, IDictionary<string, string> writeTags, string filePrefix, string fileSuffix)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public Task<bool> CreateFileAsync<TFile, TFileId, TFileContentType>(TFile sourceFile, IDictionary<string, string> fileTags, string contentType, IDictionary<string, string> writeTags, string filePrefix, string fileSuffix)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public TFile ReadFile<TFile, TFileId, TFileContentType>(string fileId, Uri tempLocation = null)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public Task<TFile> ReadFileAsync<TFile, TFileId, TFileContentType>(string fileId, Uri tempLocation = null)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public IDictionary<string, string> ReadFileMetadata<TFile, TFileId, TFileContentType>(TFile sourceFile)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public Task<IDictionary<string, string>> ReadFileMetadataAsync<TFile, TFileId, TFileContentType>(TFile sourceFile)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public TFile UpdateFile<TFile, TFileId, TFileContentType>(TFile fileToUpdate)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public Task<TFile> UpdateFileAsync<TFile, TFileId, TFileContentType>(TFile fileToUpdate)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public bool DeleteFile<TFile, TFileId, TFileContentType>(TFile fileToDelete)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public Task<bool> DeleteFileAsync<TFile, TFileId, TFileContentType>(TFile fileToDelete)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public Uri GetRelativePathForFile<TFile, TFileId, TFileContentType>(TFile file)
            where TFile : IFile<TFileId, TFileContentType>, new();

        public Uri GetFullPathForFile<TFile, TFileId, TFileContentType>(TFile file)
            where TFile : IFile<TFileId, TFileContentType>, new();

        /// <summary>
        /// Returns available storage space for this location, in bytes, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns></returns>
        public long GetAvailableStorageSpaceInBytes();


        /// <summary>
        /// Returns available storage space for this location, in GB, or -1 if unknown or "infinite" ex a cloud storage drive
        /// </summary>
        /// <returns></returns>
        public double GetAvailableStorageSpaceInGigabytes();

    }
}
