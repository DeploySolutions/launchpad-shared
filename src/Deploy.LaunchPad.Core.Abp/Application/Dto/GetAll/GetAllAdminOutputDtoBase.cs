using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class GetAllAdminOutputDtoBase<TIdType> : GetAllDetailOutputDtoBase<TIdType>
    {

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetAllAdminOutputDtoBase() : base()
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
