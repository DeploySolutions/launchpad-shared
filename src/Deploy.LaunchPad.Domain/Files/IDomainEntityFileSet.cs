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
using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Util.Metadata;
using Deploy.LaunchPad.Files;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Domain.Files
{ 
    /// <summary>
     /// A file set that can be stored and tracked as a domain entity
     /// Any type of file can be contained in the set
     /// </summary>
     /// <typeparam name="TPrimaryKey"></typeparam>
    public partial interface IDomainEntityFileSet<TPrimaryKey> :
        IDomainEntity<TPrimaryKey>

    {
        long Count { get; set; }
        IDictionary<string, IDomainEntityFile<TPrimaryKey>> Files { get; set; }
    }

    /// <summary>
    /// A file set that can be stored and tracked as a domain entity
    /// and in which the allowed files are limited to a specific content type / schema
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TFileContentType"></typeparam>
    /// <typeparam name="TSchemaFormat"></typeparam>
    public partial interface IDomainEntityFileSet<TPrimaryKey, TFileContentType, TSchemaFormat> :
        IFileSet<TFileContentType, TSchemaFormat>,
        IDomainEntity<TPrimaryKey>

    {
    }
}