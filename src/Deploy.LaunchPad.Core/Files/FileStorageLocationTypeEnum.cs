// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="FileStorageLocationTypeEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace Deploy.LaunchPad.Core.Domain
{

    /// <summary>
    /// Enum FileStorageLocationTypeEnum
    /// </summary>
    [Serializable]
    public enum FileStorageLocationTypeEnum
    {
        /// <summary>
        /// The unknown
        /// </summary>
        [Description("Unknown")]
        Unknown = -1,
        /// <summary>
        /// The windows NTFS
        /// </summary>
        [Description("Windows.NTFS")]
        Windows_NTFS = 0,
        /// <summary>
        /// The aws s3
        /// </summary>
        [Description("AWS.S3")]
        Aws_S3 = 1,
        /// <summary>
        /// The aws efs
        /// </summary>
        [Description("AWS.EFS")]
        Aws_EFS = 2,
        /// <summary>
        /// The azure BLOB storage
        /// </summary>
        [Description("Azure.BlobStorage")]
        Azure_BlobStorage = 3
    }
}
