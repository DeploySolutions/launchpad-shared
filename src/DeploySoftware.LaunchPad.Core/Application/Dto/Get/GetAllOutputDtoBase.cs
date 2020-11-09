
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Application
{
    public abstract partial class GetAllOutputDtoBase<TIdType> : ListResultDtoBase<TIdType>
    {

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllOutputDtoBase() : base()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }

        #endregion

    }
}
