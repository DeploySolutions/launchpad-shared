#region "Licensing"
/*
 * SPDX-FileCopyrightText: Copyright (c) Volosoft (https://volosoft.com) and contributors
 * SPDX-License-Identifier: MIT
 *
 * This file contains code originally from the ASP.NET Boilerplate - Web Application Framework:
 *   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
 *
 * The original portions of this file remain licensed under the MIT License.
 * You may obtain a copy of the MIT License at:
 *
 *   https://opensource.org/license/mit
 *
 *
 * SPDX-FileCopyrightText: Copyright (c) 2026 Deploy Software Solutions (https://www.deploy.solutions)
 * SPDX-License-Identifier: Apache-2.0
 *
 * Modifications and additional code in this file are licensed under
 * the Apache License, Version 2.0.
 *
 * You may obtain a copy of the Apache License at:
 *
 *   https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the Apache License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *
 * See the applicable license for governing permissions and limitations.
 */
#endregion

using Deploy.LaunchPad.Core.Threading.BackgroundWorkers;
using System;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.BackgroundJobs
{
    //TODO: Create a non-generic EnqueueAsync extension method to IBackgroundJobManager which takes types as input parameters rather than generic parameters.
    /// <summary>
    /// Defines interface of a job manager.
    /// </summary>
    public interface IBackgroundJobManager : IBackgroundWorker
    {
        /// <summary>
        /// Enqueues a job to be executed.
        /// </summary>
        /// <typeparam name="TJob">Type of the job.</typeparam>
        /// <typeparam name="TArgs">Type of the arguments of job.</typeparam>
        /// <param name="args">Job arguments.</param>
        /// <param name="priority">Job priority.</param>
        /// <param name="delay">Job delay (wait duration before first try).</param>
        /// <returns>Unique identifier of a background job.</returns>
        Task<string> EnqueueAsync<TJob, TArgs>(TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJobBase<TArgs>;

        /// <summary>
        /// Enqueues a job to be executed.
        /// </summary>
        /// <typeparam name="TJob">Type of the job.</typeparam>
        /// <typeparam name="TArgs">Type of the arguments of job.</typeparam>
        /// <param name="args">Job arguments.</param>
        /// <param name="priority">Job priority.</param>
        /// <param name="delay">Job delay (wait duration before first try).</param>
        /// <returns>Unique identifier of a background job.</returns>
        string Enqueue<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJobBase<TArgs>;

        /// <summary>
        /// Deletes a job with the specified jobId.
        /// </summary>
        /// <param name="jobId">The Job Unique Identifier.</param>
        /// <returns><c>True</c> on a successfull state transition, <c>false</c> otherwise.</returns>
        Task<bool> DeleteAsync(string jobId);

        /// <summary>
        /// Deletes a job with the specified jobId.
        /// </summary>
        /// <param name="jobId">The Job Unique Identifier.</param>
        /// <returns><c>True</c> on a successfull state transition, <c>false</c> otherwise.</returns>
        bool Delete(string jobId);
    }
}
