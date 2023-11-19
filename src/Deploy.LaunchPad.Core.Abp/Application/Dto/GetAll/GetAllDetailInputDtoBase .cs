// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetAllDetailInputDtoBase .cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.Domain.SoftwareApplications;
using System;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    /// <summary>
    /// Class GetAllDetailInputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetAllInputDtoBase{TIdType}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetAllInputDtoBase{TIdType}" />
    public abstract partial class GetAllDetailInputDtoBase<TIdType> : GetAllInputDtoBase<TIdType>
    {


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetAllDetailInputDtoBase() : base()
        {
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        public GetAllDetailInputDtoBase(int tenantId) : base()
        {
            TenantId = tenantId;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllDetailInputDtoBase{TIdType}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="culture">The culture.</param>
        public GetAllDetailInputDtoBase(int tenantId, String culture) : base()
        {
            TenantId = tenantId;
            Culture = culture;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllDetailInputDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion

    }
}
