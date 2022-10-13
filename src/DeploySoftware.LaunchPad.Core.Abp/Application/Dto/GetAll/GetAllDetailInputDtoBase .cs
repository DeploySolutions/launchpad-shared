

using DeploySoftware.LaunchPad.Core.Application.Dto;
using DeploySoftware.LaunchPad.Core.Abp.Domain.SoftwareApplications;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Abp.Application.Dto
{
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
        /// <param name="id"></param>
        public GetAllDetailInputDtoBase(int tenantId) : base()
        {
            TenantId = tenantId;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
        }

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
        protected GetAllDetailInputDtoBase(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            
        }

        #endregion

    }
}
