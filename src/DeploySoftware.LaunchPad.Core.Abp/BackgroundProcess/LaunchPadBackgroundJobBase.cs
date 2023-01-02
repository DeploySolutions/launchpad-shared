using Abp.BackgroundJobs;
using DeploySoftware.LaunchPad.Core.BackgroundProcess;

namespace DeploySoftware.LaunchPad.Core.Abp.BackgroundProcess
{
    public abstract partial class LaunchPadBackgroundJobBase<TArgs> :
        AsyncBackgroundJob<TArgs>, // ABP base async job
        IAsyncBackgroundJob<TArgs>, IBackgroundJobBase<TArgs>, IBackgroundJob<TArgs>, // ABP background job interfaces
        ICanBeLaunchPadBackgroundJob<TArgs> // LaunchPad background job interface
        where TArgs: ICanBeLaunchPadBackgroundJobArgs
    {

        protected LaunchPadBackgroundJobBase() : base() 
        { 
        
        }

        public virtual void Execute(TArgs args)
        {
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundJob_Executing, ToString()));
            ExecuteAsync(args).Wait();
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Abp_Resources.Logger_Debug_BackgroundJob_Executed, ToString()));
        }

    }
}
