// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadBackgroundJobBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.BackgroundJobs;
using Deploy.LaunchPad.Core.BackgroundProcess;

namespace Deploy.LaunchPad.Core.Abp.BackgroundProcess
{
    /// <summary>
    /// Class LaunchPadBackgroundJobBase.
    /// Implements the <see cref="AsyncBackgroundJob{TArgs}" />
    /// Implements the <see cref="IAsyncBackgroundJob{TArgs}" />
    /// Implements the <see cref="IBackgroundJobBase{TArgs}" />
    /// Implements the <see cref="IBackgroundJob{TArgs}" />
    /// Implements the <see cref="ICanBeLaunchPadBackgroundJob{TArgs}" />
    /// </summary>
    /// <typeparam name="TArgs">The type of the t arguments.</typeparam>
    /// <seealso cref="AsyncBackgroundJob{TArgs}" />
    /// <seealso cref="IAsyncBackgroundJob{TArgs}" />
    /// <seealso cref="IBackgroundJobBase{TArgs}" />
    /// <seealso cref="IBackgroundJob{TArgs}" />
    /// <seealso cref="ICanBeLaunchPadBackgroundJob{TArgs}" />
    public abstract partial class LaunchPadBackgroundJobBase<TArgs> :
        AsyncBackgroundJob<TArgs>, // ABP base async job
        IAsyncBackgroundJob<TArgs>, IBackgroundJobBase<TArgs>, IBackgroundJob<TArgs>, // ABP background job interfaces
        ICanBeLaunchPadBackgroundJob<TArgs> // LaunchPad background job interface
        where TArgs : ICanBeLaunchPadBackgroundJobArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadBackgroundJobBase{TArgs}"/> class.
        /// </summary>
        protected LaunchPadBackgroundJobBase() : base()
        {

        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public virtual void Execute(TArgs args)
        {
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundJob_Executing, ToString()));
            ExecuteAsync(args).Wait();
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundJob_Executed, ToString()));
        }

    }
}
