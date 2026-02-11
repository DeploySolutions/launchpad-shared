// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadBackgroundWorkerBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Threading.BackgroundWorkers;
using Deploy.LaunchPad.Code.BackgroundProcess;

namespace Deploy.LaunchPad.Core.Abp.BackgroundProcess
{
    /// <summary>
    /// Class LaunchPadBackgroundWorkerBase.
    /// Implements the <see cref="BackgroundWorkerBase" />
    /// Implements the <see cref="ICanBeLaunchPadBackgroundWorker" />
    /// </summary>
    /// <seealso cref="BackgroundWorkerBase" />
    /// <seealso cref="ICanBeLaunchPadBackgroundWorker" />
    public abstract partial class LaunchPadBackgroundWorkerBase : BackgroundWorkerBase, ICanBeLaunchPadBackgroundWorker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadBackgroundWorkerBase"/> class.
        /// </summary>
        protected LaunchPadBackgroundWorkerBase() : base()
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
            string message = string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Stopping, ToString());
            Logger.Debug(message);
            base.Stop();
            message = string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Stopped, ToString());
            Logger.Debug(message);
        }

        /// <summary>
        /// Note to implementors: Wait for the for the worker to finish whatever it is doing, before stopping
        /// <see cref="https://aspnetboilerplate.com/Pages/Documents/Background-Jobs-And-Workers" />
        /// </summary>
        public override void WaitToStop()
        {
            string message = string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_WaitingToStop, ToString());
            Logger.Debug(message);
            base.WaitToStop();
            message = string.Format(Deploy_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_WaitedToStop, ToString());
            Logger.Debug(message);
        }

    }
}
