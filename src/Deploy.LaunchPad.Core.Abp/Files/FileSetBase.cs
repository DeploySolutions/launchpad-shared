﻿using Deploy.LaunchPad.Core.Domain;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// A set of files all of the same type, often contained in a specific location (such as in a folder, or as a subset of files within a folder).
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    /// <typeparam name="TFileContentType"></typeparam>
    /// <typeparam name="TFileStorageLocationType"></typeparam>
    public abstract partial class FileSetBase<TIdType, TFileContentType, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public long Count { get; set; }

        public IDictionary<string, IFile<TIdType, TFileContentType>> Files { get; set; }

        public FileSetBase()
        {
            Files = new Dictionary<string, IFile<TIdType, TFileContentType>>();
        }
    }
}