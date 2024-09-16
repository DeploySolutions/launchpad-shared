// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ICanBeLaunchPadBackgroundJob.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.BackgroundProcess
{
    //
    // Summary:
    //     This interface is used to mark classes that have methods that can be run as background jobs
    //
    // Type parameters:
    //   TArgs:
    /// <summary>
    /// Interface ICanBeLaunchPadBackgroundJob
    /// </summary>
    /// <typeparam name="TArgs">The type of the t arguments.</typeparam>
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
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Execute(TArgs args);

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Task.</returns>
        public Task ExecuteAsync(TArgs args);

    }
}
