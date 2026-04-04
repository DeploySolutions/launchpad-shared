// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="FileSetBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Files.Storage;

namespace Deploy.LaunchPad.Domain.Files
{
    /// <summary>
    /// A set of files all of the same type, often contained in a specific location (such as in a folder, or as a subset of files within a folder).
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t identifier type.</typeparam>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    /// <typeparam name="TFileStorageLocationType">The type of the t file storage location type.</typeparam>
    public abstract partial class DomainEntityFileSetBase<TPrimaryKey>
        : DomainEntityBase<TPrimaryKey>, 
        IDomainEntityFileSet<TPrimaryKey> 
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public long Count { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public IDictionary<string, IDomainEntityFile<TPrimaryKey>> Files { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityFileSetBase{TPrimaryKey, TFileContentType, TFileStorageLocationType}"/> class.
        /// </summary>
        public DomainEntityFileSetBase()
        {
            Files = new Dictionary<string, IDomainEntityFile<TPrimaryKey>>();
        }
    }

    public abstract partial class DomainEntityFileSetBase<TPrimaryKey, TFileContentType, TSchemaFormat>
        : DomainEntityFileSetBase<TPrimaryKey>
    {
        
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public new IDictionary<string, IDomainEntityFile<TPrimaryKey, TFileContentType, TSchemaFormat>> Files { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityFileSetBase{TPrimaryKey, TFileContentType, TFileStorageLocationType}"/> class.
        /// </summary>
        public DomainEntityFileSetBase()
        {
            Files = new Dictionary<string, IDomainEntityFile<TPrimaryKey, TFileContentType, TSchemaFormat>>();
        }
    }
}
