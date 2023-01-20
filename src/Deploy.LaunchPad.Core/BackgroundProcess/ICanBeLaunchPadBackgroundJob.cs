﻿using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.BackgroundProcess
{
    //
    // Summary:
    //     This interface is used to mark classes that have methods that can be run as background jobs
    //
    // Type parameters:
    //   TArgs:
    public partial interface ICanBeLaunchPadBackgroundJob<in TArgs>
        where TArgs : ICanBeLaunchPadBackgroundJobArgs
    {
        //
        // Summary:
        //     Executes the job with the args.
        //
        // Parameters:
        //   args:
        //     Job arguments.
        void Execute(TArgs args);

        public Task ExecuteAsync(TArgs args);

    }
}