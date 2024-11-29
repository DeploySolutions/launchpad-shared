// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="S3File.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.Domain.Model;

namespace Deploy.LaunchPad.AWS.Abp.S3
{
    /// <summary>
    /// Class S3File.
    /// Implements the <see cref="DomainEntityFileBase{TIdType, TFileContentType}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <typeparam name="TFileContentType">The type of the t file content type.</typeparam>
    /// <seealso cref="DomainEntityFileBase{TIdType, TFileContentType}" />
    public partial class S3File<TIdType, TFileContentType> : DomainEntityFileBase<TIdType, TFileContentType>
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="S3File{TIdType, TFileContentType}"/> class.
        /// </summary>
        public S3File() : base()
        {

        }

    }
}
