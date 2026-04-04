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

namespace Deploy.LaunchPad.Files
{
    /// <summary>
    /// A collection of files, related in some fashion (by topic, format, user, etc)
    /// </summary>
    /// <typeparam name="TFileContentType"></typeparam>
    /// <typeparam name="TSchemaFormat"></typeparam>
    public partial interface IFileSet
    {
        long Count { get; set; }
        IDictionary<string, IFile> Files { get; set; }
    }
    /// <summary>
    /// A collection of files, related in some fashion (by topic, format, user, etc)
    /// but constrained to an identical content type/schema format
    /// </summary>
    /// <typeparam name="TFileContentType"></typeparam>
    /// <typeparam name="TSchemaFormat"></typeparam>
    public partial interface IFileSet<TFileContentType, TSchemaFormat> : IFileSet
    {
        new IDictionary<string, IFile<TFileContentType, TSchemaFormat>> Files { get; set; }
    }
}