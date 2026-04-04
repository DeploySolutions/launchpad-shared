using FluentResults;
using FluentValidation;
using Castle.Core.Logging;
using System.Collections.Concurrent;
using System;

namespace Deploy.LaunchPad.Code.Methods
{
    public abstract partial record LaunchPadMethodInputBase : ILaunchPadMethodInput
    {
        public virtual Guid? CorrelationId { get; init; }

        public virtual ILogger Logger { get; init; }

        /// <summary>
        /// Creates a new LaunchPadMethodInputBase for use in a method declaration to encapsulate input parameters.
        /// 
        /// </summary>
        protected LaunchPadMethodInputBase()
        {
        }

        /// <summary>
        /// Creates a new LaunchPadMethodInputBase for use in a method declaration to encapsulate input parameters.
        /// 
        /// </summary>
        protected LaunchPadMethodInputBase(ILogger logger)
        {
            if(logger != null)
            {
                Logger = logger;
            }
        }
    }

}
