﻿using System;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// The base class containing properties for all LaunchPad web client file generation processes.
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadWebClientObjectBase : LaunchPadGeneratedObjectBase
    {

        public LaunchPadWebClientObjectBase() : base()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadWebClientObjectBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            

        }

    }
}
