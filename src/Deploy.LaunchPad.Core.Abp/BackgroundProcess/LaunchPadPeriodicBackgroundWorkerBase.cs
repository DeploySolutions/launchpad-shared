// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadPeriodicBackgroundWorkerBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Deploy.LaunchPad.Core.BackgroundProcess;

namespace Deploy.LaunchPad.Core.Abp.BackgroundProcess
{
    /// <summary>
    /// Class LaunchPadPeriodicBackgroundWorkerBase.
    /// Implements the <see cref="PeriodicBackgroundWorkerBase" />
    /// Implements the <see cref="ICanBeLaunchPadBackgroundWorker" />
    /// </summary>
    /// <seealso cref="PeriodicBackgroundWorkerBase" />
    /// <seealso cref="ICanBeLaunchPadBackgroundWorker" />
    public abstract partial class LaunchPadPeriodicBackgroundWorkerBase : PeriodicBackgroundWorkerBase, ICanBeLaunchPadBackgroundWorker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadPeriodicBackgroundWorkerBase"/> class.
        /// </summary>
        /// <param name="timer">A timer.</param>
        protected LaunchPadPeriodicBackgroundWorkerBase(AbpTimer timer) : base(timer)
        {
        }

        /// <summary>
        /// Note to implementors: Make this method non-blocking
        /// <see cref="https://aspnetboilerplate.com/Pages/Documents/Background-Jobs-And-Workers" />
        /// </summary>
        public override void Start()
        {
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Starting, ToString()));
            base.Start();
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Started, ToString()));
        }

        /// <summary>
        /// Note to implementors: Make this method non-blocking
        /// <see cref="https://aspnetboilerplate.com/Pages/Documents/Background-Jobs-And-Workers" />
        /// </summary>
        public override void Stop()
        {
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Stopping, ToString()));
            base.Stop();
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Stopped, ToString()));
        }

        /// <summary>
        /// Note to implementors: Wait for the for the worker to finish whatever it is doing, before stopping
        /// <see cref="https://aspnetboilerplate.com/Pages/Documents/Background-Jobs-And-Workers" />
        /// </summary>
        public override void WaitToStop()
        {
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_WaitingToStop, ToString()));
            base.WaitToStop();
            Logger.Debug(string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_WaitedToStop, ToString()));
        }

    }
}
