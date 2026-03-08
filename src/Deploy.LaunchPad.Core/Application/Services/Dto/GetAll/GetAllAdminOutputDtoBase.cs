// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetAllAdminOutputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Application.Services.Dto.GetAll
{
    /// <summary>
    /// Class GetAllAdminOutputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Services.Dto.GetAllDetailOutputDtoBase{TPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Code.Services.Dto.GetAllDetailOutputDtoBase{TPrimaryKey}" />
    public abstract partial class GetAllAdminOutputDtoBase<TPrimaryKey> : GetAllDetailOutputDtoBase<TPrimaryKey>
    {

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllAdminOutputDtoBase() : base()
        {

        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllAdminOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion

    }
}
