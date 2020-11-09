﻿
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.Runtime.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application
{
    public abstract partial class GetAllDetailInputDtoBase<TIdType> : GetAllInputDtoBase<TIdType>
    {
       

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllDetailInputDtoBase() : base()
        {
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="id"></param>
        public GetAllDetailInputDtoBase(int tenantId, TIdType id) : base()
        {
            TenantId = tenantId;
            Id = id;
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
        }

        public GetAllDetailInputDtoBase(int tenantId, TIdType id, String culture) : base()
        {
            TenantId = tenantId;
            Id = id;
            Culture = culture;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllDetailInputDtoBase(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            
        }

        #endregion

    }
}
