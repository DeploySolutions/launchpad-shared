
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application
{
    public abstract partial class GetAllDetailOutputDtoBase<TIdType> : GetAllOutputDtoBase<TIdType>
    {

       
        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllDetailOutputDtoBase() : base()
        {
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllDetailOutputDtoBase(SerializationInfo info, StreamingContext context) :base(info,context)
        {

        }

        #endregion

    }
}
