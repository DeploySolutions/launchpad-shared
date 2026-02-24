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

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Deploy.LaunchPad.Core.Runtime.Caching
{
    /// <summary>
    /// An upper level container for <see cref="ICache"/> objects. 
    /// A cache manager should work as Singleton and track and manage <see cref="ICache"/> objects.
    /// </summary>
    public partial interface ICacheManager : ICacheManager<ICache>
    {
    }

    public partial interface ICacheManager<TCache> : IDisposable
        where TCache : class
    {
        /// <summary>
        /// Gets all caches.
        /// </summary>
        /// <returns>List of caches</returns>
        IReadOnlyList<TCache> GetAllCaches();

        /// <summary>
        /// Gets a <see cref="ICache"/> instance.
        /// It may create the cache if it does not already exists.
        /// </summary>
        /// <param name="name">
        /// Unique and case sensitive name of the cache.
        /// </param>
        /// <returns>The cache reference</returns>
        [NotNull] TCache GetCache([NotNull] string name);
    }
}
