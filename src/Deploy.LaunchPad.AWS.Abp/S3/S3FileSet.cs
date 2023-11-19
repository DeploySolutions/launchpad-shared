// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="S3FileSet.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.AWS.S3;
using Deploy.LaunchPad.Core.Abp.Domain;

namespace Deploy.LaunchPad.AWS.Abp.S3
{
    /// <summary>
    /// Class S3FileSet.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Domain.FileSetBase{TIdType, TFileContentType, Deploy.LaunchPad.AWS.S3.S3BucketStorageLocation}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Domain.FileSetBase{TIdType, TFileContentType, Deploy.LaunchPad.AWS.S3.S3BucketStorageLocation}" />
    public partial class S3FileSet<TIdType, TFileContentType> : FileSetBase<TIdType, TFileContentType, S3BucketStorageLocation>
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="S3FileSet{TIdType, TFileContentType}"/> class.
        /// </summary>
        public S3FileSet() : base()
        {
        }
    }
}
