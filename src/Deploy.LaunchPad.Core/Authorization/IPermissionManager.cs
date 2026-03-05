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

using System.Collections.Generic;
using System.Threading.Tasks;
using Deploy.LaunchPad.Core.Authorization;
using Deploy.LaunchPad.Core.MultiTenancy;

namespace Deploy.LaunchPad.Core.Authorization
{
    /// <summary>
    /// Permission manager.
    /// </summary>
    public partial interface IPermissionManager
    {
        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or throws exception
        /// if there is no permission with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        IPermission GetPermission(string name);

        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or returns null
        /// if there is no permission with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        IPermission GetPermissionOrNull(string name);

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="tenancyFilter">Can be passed false to disable tenancy filter.</param>
        IReadOnlyList<IPermission> GetAllPermissions(bool tenancyFilter = true);

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="tenancyFilter">Can be passed false to disable tenancy filter.</param>
        Task<IReadOnlyList<IPermission>> GetAllPermissionsAsync(bool tenancyFilter = true);

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="multiTenancySides">Multi-tenancy side to filter</param>
        IReadOnlyList<IPermission> GetAllPermissions(MultiTenancySides multiTenancySides);
        
        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="multiTenancySides">Multi-tenancy side to filter</param>
        Task<IReadOnlyList<IPermission>> GetAllPermissionsAsync(MultiTenancySides multiTenancySides);
    }
}
