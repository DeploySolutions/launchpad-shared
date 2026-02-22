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

using JetBrains.Annotations;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Util.Resources.Embedded
{
    /// <summary>
    /// Provides infrastructure to use any type of resources (files) embedded into assemblies.
    /// </summary>
    public interface IEmbeddedResourceManager
    {
        /// <summary>
        /// Used to get an embedded resource file.
        /// Can return null if resource is not found!
        /// </summary>
        /// <param name="fullResourcePath">Full path of the resource</param>
        /// <returns>The resource</returns>
        [CanBeNull]
        EmbeddedResourceItem GetResource([NotNull] string fullResourcePath);

        /// <summary>
        /// Used to get the list of embedded resource file.
        /// </summary>
        /// <param name="fullResourcePath">Full path of the resource</param>
        /// <returns>The list of resource</returns>
        IEnumerable<EmbeddedResourceItem> GetResources(string fullResourcePath);
    }
}