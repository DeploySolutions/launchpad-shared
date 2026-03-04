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

using Deploy.LaunchPad.Core.Runtime.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Notifications
{
    /// <summary>
    /// Used to manage notification definitions.
    /// </summary>
    public interface INotificationDefinitionManager
    {
        /// <summary>
        /// Adds the specified notification definition.
        /// </summary>
        void Add(INotificationDefinition notificationDefinition);

        /// <summary>
        /// Gets a notification definition by name.
        /// Throws exception if there is no notification definition with given name.
        /// </summary>
        INotificationDefinition Get(string name);

        /// <summary>
        /// Gets a notification definition by name.
        /// Returns null if there is no notification definition with given name.
        /// </summary>
        INotificationDefinition GetOrNull(string name);

        /// <summary>
        /// Gets all notification definitions.
        /// </summary>
        IReadOnlyList<INotificationDefinition> GetAll();

        /// <summary>
        /// Checks if given notification (<paramref name="name"/>) is available for given user.
        /// </summary>
        Task<bool> IsAvailableAsync(string name, IUserIdentifier user);

        /// <summary>
        /// Checks if given notification (<paramref name="name"/>) is available for given user.
        /// </summary>
        bool IsAvailable(string name, IUserIdentifier user);

        /// <summary>
        /// Gets all available notification definitions for given user.
        /// </summary>
        /// <param name="user">User.</param>
        Task<IReadOnlyList<INotificationDefinition>> GetAllAvailableAsync(IUserIdentifier user);

        /// <summary>
        /// Gets all available notification definitions for given user.
        /// </summary>
        /// <param name="user">User.</param>
        IReadOnlyList<INotificationDefinition> GetAllAvailable(IUserIdentifier user);

        /// <summary>
        /// Remove notification with given name
        /// </summary>
        /// <param name="name"></param>
        void Remove(string name);
    }
}