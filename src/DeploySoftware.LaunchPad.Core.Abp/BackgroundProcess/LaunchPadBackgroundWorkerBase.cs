using Abp.Threading.BackgroundWorkers;
using DeploySoftware.LaunchPad.Core.BackgroundProcess;

namespace DeploySoftware.LaunchPad.Core.Abp.BackgroundProcess
{
    public abstract partial class LaunchPadBackgroundWorkerBase : BackgroundWorkerBase, ICanBeLaunchPadBackgroundWorker
    {
        protected LaunchPadBackgroundWorkerBase() : base()
        {
        }

        /// <summary>
        /// Note to implementors: Make this method non-blocking
        /// <see cref="https://aspnetboilerplate.com/Pages/Documents/Background-Jobs-And-Workers"/>
        /// </summary>
        public override void Start()
        {
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Starting, ToString()));
            base.Start();
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Started, ToString()));
        }

        /// <summary>
        /// Note to implementors: Make this method non-blocking
        /// <see cref="https://aspnetboilerplate.com/Pages/Documents/Background-Jobs-And-Workers"/>
        /// </summary>
        public override void Stop()
        {
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Stopping, ToString()));
            base.Stop();
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_Stopped, ToString()));
        }

        /// <summary>
        /// Note to implementors: Wait for the for the worker to finish whatever it is doing, before stopping
        /// <see cref="https://aspnetboilerplate.com/Pages/Documents/Background-Jobs-And-Workers"/>
        /// </summary>
        public override void WaitToStop()
        {
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_WaitingToStop, ToString()));
            base.WaitToStop();
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundWorker_WaitedToStop, ToString()));
        }

    }
}
